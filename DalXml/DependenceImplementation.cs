namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic;
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
        XElement root = XMLTools.LoadListFromXMLElement("dependence");//Brings up the list of dependencies
        XElement newEx = new XElement("Dependence",
            new XElement("Id", Config.NextDependenceId),
            new XElement("DependentOnTask", item.DependentOnTask),
            new XElement("DependentTask", item.DependentTask));//Creates the object
        root.Add(newEx);
        XMLTools.SaveListToXMLElement(root, "dependence");
        return item.Id;
    }

    public void Delete(int id)
    {
        Dependence? removeDependence = Read(id);
        if (removeDependence == null)//if not found
            throw new DalDoesNotExistException($"A Dependence With Number= {id} Does Not Exist");
        XMLTools.LoadListFromXMLElement("dependence").Elements().FirstOrDefault(x=>(int)x.Element("Id")! == id)!.Remove();//Deletes the dependency from the list
    }

    public Dependence? Read(int id)
    {
        XElement? readEx = XMLTools.LoadListFromXMLElement("dependence").Elements().FirstOrDefault(x => (int)x.Element("Id")! == id);// Brings the requested dependency
        if (readEx == null)
            return null;
        Dependence dependence = new Dependence ((int)readEx.Element("Id")!, (int)readEx.Element("DependentOnTask")!, (int)readEx.Element("DependentTask")!);
        return dependence;//Creates a new object according to the data saved to me and returns it
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        Dependence? dependence = XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!)).FirstOrDefault(filter!); 
        return dependence;//Creates a new object according to the data stored in me and returns it if it meets the filter conditions
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter=null)
    {
        if(filter == null)//If there is no filter - returns all dependencies
            return XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!));
        return//Returns all dependencies according to the filter
         XMLTools.LoadListFromXMLElement("dependence").Elements("Dependence")!.Select(ex =>
            new Dependence((int)ex!.Element("Id")!, (int)ex!.Element("DependentOnTask")!, (int)ex!.Element("DependentTask")!)).Where(filter!);
    }

    public void Update(Dependence item)
    {
        Delete(item.Id);//Deletes the dependency before the update
        Create(item);//Saves the updated dependency
    }

    public void Reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement("dependence");//Brings all dependencies
        root.Elements().Remove();//Removes them all
        XMLTools.SaveListToXMLElement(root, "dependence");//Save
        //initialization
        XElement xConfig = XMLTools.LoadListFromXMLElement("data-config");
        xConfig.Element("NextDependenceId")!.SetValue(1);
        XMLTools.SaveListToXMLElement(xConfig, "data-config");// Resaves the empty list
    }
}
