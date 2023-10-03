using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerDetails
/// </summary>
public class CustomerDetails
{
    public int Id { get; set; }

    public string Customer_Code { get; set; }

    public string Customer_Name { get; set; }

    public string Channel { get; set; }

    public string Category { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Route { get; set; }

    public string HQ { get; set; }

    public string Janv { get; set; }

    public string Febv { get; set; }

    public string Marv { get; set; }

    public string Aprv { get; set; }

    public string Mayv { get; set; }

    public string Junv { get; set; }

    public string Julv { get; set; }

    public string Augv { get; set; }

    public string Sepv { get; set; }

    public string Octv { get; set; }

    public string Novv { get; set; }

    public string Decv { get; set; }

    public string Jan { get; set; }

    public string Feb { get; set; }

    public string Mar { get; set; }

    public string Apr { get; set; }

    public string May { get; set; }

    public string Jun { get; set; }

    public string Jul { get; set; }

    public string Aug { get; set; }

    public string Sep { get; set; }

    public string Oct { get; set; }

    public string Nov { get; set; }

    public string Dec { get; set; }

    public string Total { get; set; }
}

public class DataTableResponse
{
    public int draw { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
    public List<CustomerDetails> data { get; set; }
}

public class yearlist
{
    public string Value { get; set; }
    public string Text { get; set; }
}

public class SudivList
{
    public string subdivision_name { get; set; }
    public int subdivision_code { get; set; }
}


public class UserList
{
    public string sf_name { get; set; }
    public string sf_Code { get; set; }
}