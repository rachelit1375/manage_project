namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)//If there is such an engineer already
            throw new DalAlreadyExistsException($"An Engineers With Id= {item.Id} Already Exist");
        XElement root=  XMLTools.LoadListFromXMLElement("engineer");
        XElement newEx = new XElement("Engineer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Email", item.Email),
            new XElement("Level", item.Level),
            new XElement("Cost", item.Cost),
            new XElement("Active", item.Active)
            );
        root.Add(newEx);
        XMLTools.SaveListToXMLElement(root, "engineer");
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("You cannot delete engineer");
    }

    public Engineer? Read(int id)
    {
        XElement? ex =  XMLTools.LoadListFromXMLElement("engineer").Elements("Engineer").FirstOrDefault(x => (int)x.Element("Id")! == id);
        if (ex == null)
            return null;
        int level = (int)ex!.Element("Level")!;
        Engineer engineer = new Engineer((int)ex!.Element("Id")!, (string)ex!.Element("Name")!, (string)ex!.Element("Email")!, (DO.EngineerExperience)level, (double)ex!.Element("Cost")!, (bool)ex!.Element("Active")!);
        return engineer;//Returns the engineer
    }

    public Engineer? Read(Func<Engineer, bool>? filter = null)
    {//
        XElement? ex = XMLTools.LoadListFromXMLElement("engineer").Elements("Engineer").Select(ex =>
            new Engineer((int)ex!.Element("Id")!, (string)ex!.Element("Name")!, (string)ex!.Element("Email")!, (DO.EngineerExperience)ex!.Element("Level")!, (double)ex!.Element("Cost")!, (bool)ex!.Element("Active")!)).FirstOrDefault(filter);
        int level = (int)ex!.Element("Level")!;
        Engineer engineer = new Engineer((int)ex!.Element("Id")!, (string)ex!.Element("Name")!, (string)ex!.Element("Email")!, (DO.EngineerExperience)level, (double)ex!.Element("Cost")!, (bool)ex!.Element("Active")!);
        return engineer;//Returns the engineer
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter)
    {
        return null;
    }

    public void Update(Engineer item)
    {
        
    }
}
