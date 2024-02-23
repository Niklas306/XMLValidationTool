using System.Xml;
using System.Xml.Schema;

Console.WriteLine("Willkommen beim XML Validation Tool!\n\nGeben Sie den Pfad des XSD Schemas ein:");

string XSDschemaInput = Console.ReadLine();

Console.WriteLine("\nGeben Sie den Pfad der XML Datei zur Validierung ein:");

string XMLfileInput = Console.ReadLine();

if (!string.IsNullOrEmpty(XSDschemaInput) && !string.IsNullOrEmpty(XMLfileInput))
{
    Console.WriteLine("Starte Validierung...");
    Validate(XSDschemaInput, XMLfileInput);
}

Console.ReadLine();


void Validate(string xsdpath, string xmlpath)
{
    try
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

        XmlReader reader = XmlReader.Create(xmlpath, settings);
        XmlDocument document = new XmlDocument();
        document.Load(reader);

        document.Validate(ValidationCallback);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ausnahme: " + ex.Message + " ausgelöst!");
    }
}

static void ValidationCallback(object sender, ValidationEventArgs args)
{
    if (args.Severity == XmlSeverityType.Warning)
    {
        Console.WriteLine("\tWarning: Matching schema not found. No validation occured." + args.Message);
    } else
    {
        Console.WriteLine("\tValidation error: " + args.Message);
    }
}