using DatabaseDAL;
using System;
using System.Collections.Generic;

namespace DAL;

public partial class Account : TheFactory_Entity
{
    public int accountID { get; set; }

    public string? groupPermission { get; set; }

    public string? email { get; set; }

    public string accountName { get; set; } = null!;

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    public virtual ICollection<Form> Forms { get; } = new List<Form>();

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
