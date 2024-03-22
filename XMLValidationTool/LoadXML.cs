using System.Xml;
using System.Xml.Schema;

namespace XMLValidationTool
{
    public class LoadXML
    {
        Properties properties = new Properties();

        public bool ProcessXMLFile (string xmlFilePath, string schemaFilePath)
        {
            try
            {
                var settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, schemaFilePath);
                settings.ValidationEventHandler += ValidationCallback;

                using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
                {
                    while (reader.Read())
                    {
                        //Validierung findet automatisch statt - Ausgabe des aktuellen Schrittes
                    }
                }
                    return true;
            }
            catch (Exception ex)
            {
                properties.ValidationMessage = $"Fehler beim Validieren der XML: {ex.Message}";
                return false;
            }
        }

        private void ValidationCallback(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                properties.ValidationMessage = $"{e.Severity}: {e.Message}";
            }
        }
    }
}
