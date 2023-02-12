using DatabaseDAL;
using System;
using System.Collections.Generic;

namespace DAL;

public partial class Form : TheFactory_Entity
{
    public int formID { get; set; }

    public int? accountID { get; set; }

    public string? companyName { get; set; }

    public string? repName { get; set; }

    public DateTime callDate { get; set; }

    public string? timeLength { get; set; }

    public string? callDesc { get; set; }

    public string? issueSolved { get; set; }

    public string? followUp { get; set; }

    public virtual Account? account { get; set; }
}
