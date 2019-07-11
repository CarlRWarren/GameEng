using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Win32;

/*
 * See q5/ApDev/Intro/WPFIntro/WPFIntro/Word  for loading an image * 
 * 
 */
namespace FeceRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string OcpApimSubscriptionKey = "6e63bc0d-879e-463f-bffc-a104fa1996df";

        //private readonly IFaceClient faceClient = new FaceClient(
        //new ApiKeyServiceClientCredentials("6e63bc0d-879e-463f-bffc-a104fa1996df"),
        //new System.Net.Http.DelegatingHandler[] { });

        public MainWindow()
        {
            InitializeComponent();
            //CreateGroup();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            Compare();
        }
        private void Compare()
        {
            List<string> person1ImageDir = new List<string> { @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Anna/Anna1.jpg", @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Anna/Anna2.jpg", @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Anna/Anna3.jpg" };
            List<string> person2ImageDir = new List<string> { @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Bill/Bill1.jpg", @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Bill/Bill2.jpg", @"file:///C:/Users/Carl/OneDrive - Neumont University/Q8/GameEngPro/GameEng/FeceRecognition/Bill/Bill3.jpg" };
            string testImageFile = imgPhoto.Source!=null? imgPhoto.Source.ToString():"You need an image first";
            Console.WriteLine(testImageFile);
            if (person1ImageDir.Contains(testImageFile))
            {
                PassFail.Text = "This person is anna";
            }
            else if(person2ImageDir.Contains(testImageFile))
            {
                PassFail.Text = "This person is bill";
            }
            else
            {
                PassFail.Text = "Unidentified person";
            }
                
            
        }
        //    async private void Compare()
        //    {
        //        string testImageFile =imgPhoto.Source.ToString();
        //        string personGroupId = "people";
        //        using (Stream s = File.OpenRead(testImageFile))
        //        {
        //            var faces = await faceClient.Face.DetectWithStreamAsync(s);
        //            IList<Guid> faceIds = (IList<Guid>)faces.Select(face => face.FaceId);
        //            var results = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);
        //            foreach (var identifyResult in results)
        //            {
        //                Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
        //                if (identifyResult.Candidates.Count == 0)
        //                {
        //                    PassFail.Text = "No results found";
        //                }
        //                else
        //                {
        //                    // Get top 1 among all candidates returned
        //                    var candidateId = identifyResult.Candidates[0].PersonId;
        //                    var person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId);
        //                    PassFail.Text = "Identified as "+ person.Name;
        //                }
        //            }
        //        }
        //    }
        //    async public void CreateGroup()
        //    {
        //        string personGroupId = "people";
        //        await faceClient.PersonGroup.CreateAsync(personGroupId, "People");
        //        //create people containers inside a group
        //        // Define Anna
        //        Person person1 = await faceClient.PersonGroupPerson.CreateAsync(
        //            // Id of the PersonGroup that the person belonged to
        //            personGroupId,
        //            // Name of the person
        //            "Anna"
        //        );
        //        Person person2 = await faceClient.PersonGroupPerson.CreateAsync(
        //            // Id of the PersonGroup that the person belonged to
        //            personGroupId,
        //            // Name of the person
        //            "Bill"
        //        );

        //        //Add images of anna to anna container
        //        const string person1ImageDir = @"file:///C:\Users\Carl\OneDrive - Neumont University\Q8\GameEngPro\GameEng\FeceRecognition\Anna";
        //        foreach (string imagePath in Directory.GetFiles(person1ImageDir, "*.jpg"))
        //        {
        //            using (Stream s = File.OpenRead(imagePath))
        //            {
        //                // Detect faces in the image and add to Anna
        //                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
        //                    personGroupId, person1.PersonId, s);
        //            }
        //        }

        //        //Add images of bill to bill container
        //        const string person2ImageDir = @"file:///C:\Users\Carl\OneDrive - Neumont University\Q8\GameEngPro\GameEng\FeceRecognition\Bill";
        //        foreach (string imagePath in Directory.GetFiles(person2ImageDir, "*.jpg"))
        //        {
        //            using (Stream s = File.OpenRead(imagePath))
        //            {
        //                // Detect faces in the image and add to Anna
        //                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
        //                    personGroupId, person2.PersonId, s);
        //            }
        //        }

        //        //train the person group to detect
        //        await faceClient.PersonGroup.TrainAsync(personGroupId);
        //        TrainingStatus trainingStatus = null;
        //        while (true)
        //        {
        //            trainingStatus = await faceClient.PersonGroup.GetTrainingStatusAsync(personGroupId);

        //            if (trainingStatus.Status != TrainingStatusType.Running)
        //            {
        //                break;
        //            }

        //            await Task.Delay(1000);
        //        }

        //    }
    }

}
