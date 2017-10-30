using Dokumenter.Domain.Factories;
using SendDokumenter.iis.DocumentDataReference;
using SendDokumenter.iis.FileHandling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SendDokumenter.iis
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = ConfigurationManager.AppSettings["folder"].ToString();   // @"C:\work\bruger\fifa\ESDH\DigitalPost\Dokumenter.Domain\Documents";

            string current = Directory.GetCurrentDirectory();

            string[] filePaths = Directory.GetFiles(folder);

            AttachedDocuments att = new AttachedDocuments
            {
                EmailAddress = "julemanden@godthaab.dk",
                OrganizationUnitName = "organiseret",
                Emneord = "lønforhøjelse",
                Medlemsnummer = 123456,
                Name = "Boligministeriet",
                PrimaryClass = "første klasses",
                ProductionUnit = "værkstedet",
                Sagsnummer = "17/444444",
                SecondaryClass = "anden klasses",                 
            };

            att.AttachedFiles = Files(filePaths);

            string url = "http://srv-udv-114/SendEboksService/DocumentData.svc";

            BindingFactory bindingFactory = new BindingFactory();
            EndpointAddress epa = new EndpointAddress(url);

            string bindingName = "BasicHttpBinding_IDocumentData";

            BasicHttpBinding basicBinding = bindingFactory.CreateBasicHttpBinding(bindingName);

            DocumentDataClient client = new DocumentDataClient();

            var svar = client.SendAttachments(att);

        }

        static AttachedFile[] Files(string[] filePaths)
        {
            List<AttachedFile> files = new List<AttachedFile>();

            FileBytes fileBytes = new FileBytes();

            foreach (string fp in filePaths)
            {
                var att = fileBytes.CreateAttachment(fp);
                files.Add(att);
            }

            return files.ToArray();
        }
    }
}
