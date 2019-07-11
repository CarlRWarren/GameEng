using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Win32;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        private readonly IFaceClient faceClient = new FaceClient(
        new ApiKeyServiceClientCredentials("6e63bc0d-879e-463f-bffc-a104fa1996df"),
        new System.Net.Http.DelegatingHandler[] { });

        public Form1()
        {
            InitializeComponent();
            //CreateGroup();
        }

        async public void CreateGroup()
        {
            string personGroupId = "people";

            await faceClient.PersonGroup.CreateAsync(personGroupId, "People");
            //create people containers inside a group
            // Define Anna
            Person person1 = await faceClient.PersonGroupPerson.CreateAsync(
                // Id of the PersonGroup that the person belonged to
                personGroupId,
                // Name of the person
                "Anna"
            );
            Person person2 = await faceClient.PersonGroupPerson.CreateAsync(
                // Id of the PersonGroup that the person belonged to
                personGroupId,
                // Name of the person
                "Bill"
            );

            //Add images of anna to anna container
            const string person1ImageDir = @"C:\Users\Carl\OneDrive - Neumont University\Q8\GameEngPro\GameEng\PictureViewer\Anna";
            foreach (string imagePath in Directory.GetFiles(person1ImageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    // Detect faces in the image and add to Anna
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                        personGroupId, person1.PersonId, s);
                }
            }

            //Add images of bill to bill container
            const string person2ImageDir = @"C:\Users\Carl\OneDrive - Neumont University\Q8\GameEngPro\GameEng\PictureViewer\Bill";
            foreach (string imagePath in Directory.GetFiles(person2ImageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    // Detect faces in the image and add to Anna
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                        personGroupId, person2.PersonId, s);
                }
            }

            //train the person group to detect
            await faceClient.PersonGroup.TrainAsync(personGroupId);
            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceClient.PersonGroup.GetTrainingStatusAsync(personGroupId);

                if (trainingStatus.Status != TrainingStatusType.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }

            //identify a face against a defined person group
            string testImageFile = @"C:\Users\Carl\OneDrive - Neumont University\Q8\GameEngPro\GameEng\FeceRecognition\Anna\Anna1.jpg";
            using (Stream s = File.OpenRead(testImageFile))
            {
                var faces = await faceClient.Face.DetectWithStreamAsync(s);
                IList<Guid> faceIds = (IList<Guid>)faces.Select(face => face.FaceId);
                var results = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);
                foreach (var identifyResult in results)
                {
                    Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                    if (identifyResult.Candidates.Count == 0)
                    {
                        Console.WriteLine("No one identified");
                    }
                    else
                    {
                        // Get top 1 among all candidates returned
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId);
                        Console.WriteLine("Identified as {0}", person.Name);
                    }
                }
            }

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        async private void compareButton_Click(object sender, EventArgs e)
        {
            string testImageFile = pictureBox1.ImageLocation;
            string personGroupId = "people";
            using (Stream s = File.OpenRead(testImageFile))
            {
                var faces = await faceClient.Face.DetectWithStreamAsync(s);
                IList<Guid> faceIds = (IList<Guid>)faces.Select(face => face.FaceId);
                var results = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);
                foreach (var identifyResult in results)
                {
                    Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                    if (identifyResult.Candidates.Count == 0)
                    {
                        pictureBox1.BackColor = Color.Red;
                    }
                    else
                    {
                        // Get top 1 among all candidates returned
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId);
                        pictureBox1.BackColor = Color.Green;
                    }
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
