using System.Xml.Linq;

public class PrintSettingsLoader
{
    public static PrintSettings LoadPrintSettings(string filePath)
    {
        var xml = XDocument.Load(filePath);
        var dimensions = xml.Element("PrintSettings").Element("Dimensions");
        var content = xml.Element("PrintSettings").Element("Content");

        double width = double.Parse(dimensions.Element("Width").Value);
        double height = double.Parse(dimensions.Element("Height").Value);

        string separator = content.Element("Separator").Value;
        string serviceName = content.Element("ServiceName").Value;
        string servicePrice = content.Element("ServicePrice").Value;
        string quantity = content.Element("Quantity").Value;
        string total = content.Element("Total").Value;
        string employee = content.Element("Employee").Value;

        return new PrintSettings
        {
            Width = width,
            Height = height,
            Separator = separator,
            ServiceName = serviceName,
            ServicePrice = servicePrice,
            Quantity = quantity,
            Total = total,
            Employee = employee
        };
    }
}

public class PrintSettings
{
    public double Width { get; set; }
    public double Height { get; set; }
    public string Separator { get; set; }
    public string ServiceName { get; set; }
    public string ServicePrice { get; set; }
    public string Quantity { get; set; }
    public string Total { get; set; }
    public string Employee { get; set; }
}
