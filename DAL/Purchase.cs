using DatabaseDAL;
using System;
using System.Collections.Generic;

namespace DAL;

public partial class Purchase : TheFactory_Entity
{
    public int Purchase_ID { get; set; }

    public int accountID { get; set; }

    public DateTime purchaseDate { get; set; }

    public string? supplier { get; set; }

    public int quantity { get; set; }

    public decimal? productPrice { get; set; }

    public decimal? tax { get; set; }

    public decimal? net { get; set; }

    public decimal? totalAfterTax { get; set; }

    public string? reference { get; set; }

    public string? status { get; set; }

    public int? accountID_approver { get; set; }

    public virtual Account? account { get; set; }
}
