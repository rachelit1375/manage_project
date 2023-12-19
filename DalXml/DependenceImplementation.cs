namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        if (Read(item.Id) != null)//If there is such an engineer already
            throw new DalAlreadyExistsException($"An Dependences With Id= {item.Id} Already Exist");
        XElement root = XMLTools.LoadListFromXMLElement("dependence");
        XElement newEx = new XElement("Dependence",
            new XElement("Id", item.Id),
            new XElement("DependentOnTask", item.DependentOnTask),
            new XElement("DependentTask", item.DependentTask));
        root.Add(newEx);
        XMLTools.SaveListToXMLElement(root, "dependence");
        return item.Id;
    }

    public void Delete(int id)
    {
        Dependence? removeDependence = Read(id);
        if (removeDependence == null)
            throw new DalDoesNotExistException($"A Dependence With Number= {id} Does Not Exist");
        XMLTools.LoadListFromXMLElement("dependence").Elements().FirstOrDefault(x=>(int)x.Element("Id")! == id)!.Remove();
    }

    public Dependence? Read(int id)
    {
        XElement? readEx = XMLTools.LoadListFromXMLElement("dependence").Elements().FirstOrDefault(x => (int)x.Element("Id")! == id);
        if(readEx == null)
            return null;
        Dependence dependence=new Dependence ((int)readEx.Element("Id")!, (int)readEx.Element("DependentOnTask")!, (int)readEx.Element("DependentTask")!);
        return dependence;
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        Dependence? dependence = XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!)).FirstOrDefault(filter!); 
        return dependence;
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter=null)
    {
        if(filter == null)
            return XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!));
        return
         XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!)).Where(filter!);
    }

    public void Update(Dependence item)
    {
        Delete(item.Id);
        Create(item);
    }
}
