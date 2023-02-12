global using Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using DAL;
using DatabaseDAL;
using Microsoft.SqlServer.Server;


public class ProfilesViewModels
{
    readonly private AccountDAO _dao;
    public int accountID { get; set; }

    public string? employeeName { get; set; }

    public string? accountName { get; set; }

    public string? accountPassword { get; set; }

    public string? groupPermission { get; set; }



    public ProfilesViewModels()
    {
        _dao = new AccountDAO();
    }

    public async Task<Account> checkUsernameUse()
    {
        try
        {
            Account acc = await _dao.checkUsernameUse(accountName);
            return acc;
        }
        catch (NullReferenceException nex)
        {
            Debug.WriteLine(nex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
             MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
       
        return null;
    }

    public async Task<int> checkLogin()
    {
        int value = -1 ;
        try
        {
            Account acc = await _dao.checkLogin(accountName, accountPassword);
            if (acc != null)
            {
                if (acc.accountID < 0)
                {
                    return -2; //username is in use
                }
                else
                {
                    value = acc.accountID;
                }
            }
            
        }
        catch (NullReferenceException nex)
        {
            Debug.WriteLine(nex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
             MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        
        return value;

    }

    public async Task<int> AddAccount()
    {

        try
        {
            Account newAccount = new()
            {
                username = accountName,
                password = accountPassword  ,
                accountName = employeeName, 
                groupPermission = groupPermission
            };
            accountID = await _dao.addAccount(newAccount);
            return accountID;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<int> Delete()
    {
        try
        {
            // dao will return # of rows deleted
            return await _dao.Delete((int)accountID!);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }

    public async Task<List<ProfilesViewModels>> GetAll()
    {
        List<ProfilesViewModels> allVms = new();
        try
        {
            List<Account> allProfiles = await _dao.GetAll();
           
            foreach (Account stu in allProfiles)
            {
                ProfilesViewModels stuVm = new()
                {
                    accountID = stu.accountID,
                    accountName = stu.username,
                    accountPassword = stu.password,
                    employeeName = stu.accountName,
                    groupPermission = stu.groupPermission
                };
               
                allVms.Add(stuVm);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return allVms;
    }

    public async Task<int> Update()
    {
        int updatestatus;
        try
        {
            Account acc = new Account();
            acc.accountID = accountID;
            acc.username = accountName;
            acc.password = accountPassword;
            acc.groupPermission = groupPermission;

          
            updatestatus = (int)await _dao.Update(acc);

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }


        return updatestatus;
    }


    public async Task<Account> getAccountById(int accountID)
    {
        Account acc = new Account();
        acc = await _dao.getAccountByID(accountID);

        if(acc == null)
        {
            return null;
        }
        else
        {
            return acc;
        }

    }




}