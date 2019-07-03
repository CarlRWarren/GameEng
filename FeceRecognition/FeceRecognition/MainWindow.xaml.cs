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
        string OcpApimSubscriptionKey = "6e63bc0d-879e-463f-bffc-a104fa1996df";
        
        private readonly IFaceClient faceClient = new FaceClient(
        new ApiKeyServiceClientCredentials("6e63bc0d-879e-463f-bffc-a104fa1996df"),
        new System.Net.Http.DelegatingHandler[] { });
        
        public MainWindow()
        {
            InitializeComponent();
            CreateGroup();
        }
        async public void CreateGroup()
        {
            string personGroupId = "people";
            await faceClient.PersonGroup.CreateAsync(personGroupId, "People");

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

            const string person1ImageDir = @"../Anna";

            foreach (string imagePath in Directory.GetFiles(person1ImageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    // Detect faces in the image and add to Anna
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                        personGroupId, person1.PersonId, s);
                }
            }

            const string person2ImageDir = @"../Bill";

            foreach (string imagePath in Directory.GetFiles(person2ImageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    // Detect faces in the image and add to Anna
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                        personGroupId, person2.PersonId, s);
                }
            }
        }
    }
}
