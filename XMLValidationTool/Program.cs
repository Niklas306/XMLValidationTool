using XMLValidationTool;

Properties properties = new Properties();

StartProgram();



void StartProgram()
{
    Console.WriteLine("Welcome to the XML Validation Tool!\n\nEnter the path of the XSD schema:");

    string XSDschemaInput = Console.ReadLine();

    Console.WriteLine("\nEnter the path of the XML file for validation:");

    string XMLfileInput = Console.ReadLine();

    if (!string.IsNullOrEmpty(XSDschemaInput) && !string.IsNullOrEmpty(XMLfileInput))
    {
        Console.WriteLine("\nstart validation...\n");
        LoadXML xmlValidator = new LoadXML();
        bool isValid = xmlValidator.ProcessXMLFile(XMLfileInput, XSDschemaInput);

        if (isValid)
        {
            Console.WriteLine("validation successful!\n");
        } else
        {
            Console.WriteLine("validation unsuccessful: " + properties.ValidationMessage + "\n");
        }

        Console.Write("should another file be validated? Type YES or NO: ");
        properties.UserInput = Console.ReadLine();
        NextValidation();
    }
}

//After the current validation has ended, the user is asked whether another validation should take place
void NextValidation()
{
    if (properties.UserInput.Equals("YES"))
    {
        Console.Clear();
        StartProgram();
    }
    else if (properties.UserInput.Equals("NO"))
    {
        Environment.Exit(0);
    }
    else
    {
        Console.Clear();
        Console.WriteLine("\nplease enter a valid answer\n\n");

        Console.Write("should another file be validated? Type YES or NO: ");
        properties.UserInput = Console.ReadLine();
        NextValidation();
    }
}