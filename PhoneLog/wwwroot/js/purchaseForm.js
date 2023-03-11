



$(() => { // main jQuery routine - executes every on page load, $ is short for jquery

    $("#loginButton").click(async (e) => {
        var username = $("#usernameTF").val();
        var password = $("#passwordTF").val();
        //check if its validloglist
        try {
            let testBody;            
            let response = await fetch(`api/login/${username},${password}`, {
                method: "GET",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(testBody),
            });

            let myData = await response.json();
            myData = JSON.stringify(myData);
            $("#searchNav").show()


         if (myData >=0) {
             sessionStorage.setItem("accountID", myData)

             let getPermission = await fetch(`api/login/getUserByID/${myData}`)
             let permission = null
             if (getPermission.ok) {
                 let payload = await getPermission.json(); // th
                 permission = payload.groupPermission
             }
             sessionStorage.setItem("Permission", permission)

             $("#loginContainer").hide();
             $("#userList").show(); 
             getAll("");
          } else {
             $("#LoginStatus").text("Credentials are not valid.")
         }
        } catch (error) {
            // catastrophic
            
            console.log("error " + error)
        }

        let getAdmins = await fetch(`api/Accounts`);
        if (getAdmins.ok) {
            let payload = await getAdmins.json(); // this returns a promise, so we await it
            console.log("payLoad is " + JSON.stringify(payload))

            payload.forEach(async admin => {
                btn = $(` <option value="${admin.username}">${admin.accountName}</option>`);
                btn.appendTo($("#emailReceiverOption"));

            }); // 
        }



    });

    $("#buttonDiv").click((e) => {
        let buttonClicked = e.target.id

        if (buttonClicked==="AddUser") {
            $("#addModal").modal("toggle");
        } else if (buttonClicked === "UpdateAccount") {
            clearUpdateModal();
            $("#updateModal").modal("toggle");
        }
        else if (buttonClicked === "CreateForm") {
            $("#myModal").modal("toggle");
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();
            today = mm + '/' + dd + '/' + yyyy;
            
        }

    });

    $("#addUsername").click(async (e) => {
        var username = $("#TextBoxUsername").val();
        var password = $("#TextBoxUserPassword").val();
        var name = $("#TextBoxName").val();
        var permissionGroup = $("#accountAccess").val();

        //check if its valid
        try {

            //check if username is in use
            if (username.length < 4) {
                $("#addUserStatus").text("username needs more then 4 characters");
            } else {
                let responseID
                let response = await fetch(`api/login/checkUsername/${username}`, {
                    method: "GET",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(responseID),
                });
                responseID = await response.json();
                responseID = JSON.stringify(responseID);

                if (responseID >=0) {
                    //username is already in use
                    //status username in use.
                    $("#addUserStatus").text("Username already in use");

                } else if (password.length < 4) {
                    $("#addUserStatus").text("password needs more then 4 characters");
                } else {
                    //username can be used ;) 
              
                    let testBody
                    let response = await fetch(`api/login/${username},${password},${name},${permissionGroup}`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json; charset=utf-8" },
                        body: JSON.stringify(testBody),
                    });


                    let myData = await response.json();
                    myData = JSON.stringify(myData);

                    
                    $("#TextBoxName").val("")
                    $("#TextBoxUsername").val("");
                    $("#TextBoxUserPassword").val("");
                    $("#status").text("Account  " + username + " was inserted in the system")
                    $("#addModal").modal("toggle");


                }

            } 
        }catch (error) {
                // catastrophic
                console.log("error " + error)
         }

    });

    $("#recoverUser").click(async (e) => {


        var username = $("#Recover_TextBoxUsername").val();
        var password = $("#Recover_TextBoxUserPassword").val();
        var password_confirm = $("#Recover_TextBoxUserPassword_confirm").val();

        try {

            //check if username exists
            let responseID
            let response = await fetch(`api/login/checkUsername/${username}`, {
                method: "GET",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(responseID),
            });

            responseID = await response.json();
            responseID = JSON.stringify(responseID);



            if (responseID < 0) {
                //notify account doesnt exist 
                $("#updateStatus").text("Username does not exist.")
            } else {
                //confirms if both passwords matches

             
                if (password !== password_confirm) {
                    $("#updateStatus").text("Both passwords must match.");

                    
                } else if (password.length <= 3) {
                    $("#updateStatus").text("username needs more then 4 characters");

                } else {
                   //if matches, update password using fetch,display success message and toggle it off
                    
                    let responseRecover = await fetch(`api/login/${username},${password}`, {
                        method: "PUT",
                        headers: { "Content-Type": "application/json; charset=utf-8" },
                        body: JSON.stringify(responseID),
                    });
                    responseID = await responseRecover.json();
                    responseID = JSON.stringify(responseID);
                    if (responseID >= 1) {
                        $("#status").text("Account  " + username + " had password updated")
                        $("#updateModal").modal("toggle");

       

                    } else {
                        //probably the password is the same as previously
                        $("#updateStatus").text("username already has that password");

                    }


                }


            }

        } catch (error) {
            console.log("error??")

        }
    });


    $("#SubmitForm").click(async (e) => {

        let accountID = sessionStorage.getItem("accountID");

        //check approval checkbox
        if (document.getElementById('needApproval').checked == false && document.getElementById('noApproval').checked == false) {
            $("#modalstatus").text("Approval type needs to be selected")

            return
        }

        let status

        myPurchase = new Object();
        myPurchase.accountID = accountID;
        myPurchase.supplier = $("#TextBoxSupplier").val();
        myPurchase.quantity = $("#TextBoxQuantity").val();
        myPurchase.productPrice = $("#TextBoxPrice").val();
        myPurchase.reference = $("#TextBoxReference").val();
        myPurchase.net = myPurchase.productPrice * myPurchase.quantity;

        if (document.getElementById('includeTax').checked) {
            myPurchase.tax = 1.13
            myPurchase.totalAfterTax = myPurchase.tax * myPurchase.net
        } else if (document.getElementById('noTax').checked) {
            myPurchase.tax = 0
            myPurchase.totalAfterTax = myPurchase.net
        } else {
            checkBoxNotMaked = false;
        }

        if (document.getElementById('noApproval').checked && myPurchase.totalAfterTax < 200) {
            status = "approved"
            myPurchase.accountID_approver = accountID

        } else {
            console.log("status was set")
            status = "pending"
        }
        myPurchase.status = status
        myPurchase.purchaseDate = new Date();

        let response = await fetch("api/purchase", {
            method: "post",
            headers: {
                "content-type": "application/json; charset=utf-8"
            },
            body: JSON.stringify(myPurchase)
        });

        if (document.getElementById('needEmail').checked == true) {
            let emailSelected = $("#emailReceiverOption").val()

            let sendEmail = await fetch(`api/Email/${emailSelected}`, {
                method: "post",
                headers: {
                    "content-type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(myPurchase)
            });
        }

        console.log(JSON.stringify(myPurchase))
        $("#myModal").modal("toggle");
        getAll("");

        $("#emailReceiverOption").val("Select One")
        $("#TextBoxSupplier").val("")
        $("#TextBoxQuantity").val("")
        $("#TextBoxPrice").val("")
        $("#TextBoxReference").val("")
        $("#TextBoxTotalAfterTax").val("")
        $("#includeTax").prop("checked", false);
        $("#noTax").prop("checked", false);
        $("#noApproval").prop("checked", false);
        $("#needApproval").prop("checked", false);
    });

    //update purchaseForm
    $("#updateStatusDiv").click(async (e) => {
        let buttonClicked = e.target.id

        if (buttonClicked === "actionbutton") {
          let accountID = sessionStorage.getItem("accountID");
          let purchaseID = sessionStorage.getItem("formID")
          let status = $("#formStatusUpdate").val()

          let responseID
          let responseRecover = await fetch(`api/Purchase/${status},${purchaseID},${accountID}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(responseID),

        
          });

        $("#logModal").modal("toggle");
        getAll("");
        }
    })

    $("#needApproval").click(() => {
        $("#noApproval").prop("checked", false);
        updateTotal()
    });

    $("#noApproval").click(() => {
        $("#needApproval").prop("checked", false);
        updateTotal()
    })

    $("#needEmail").click(() => {
        $("#noEmail").prop("checked", false);
        updateTotal()
    });

    $("#noEmail").click(() => {
        $("#needEmail").prop("checked", false);
        updateTotal()
    })

    $("#includeTax").click(() => {
        $("#noTax").prop("checked", false);
        updateTotal()
    });

    $("#noTax").click(() => {
        $("#includeTax").prop("checked", false);
        updateTotal()
    })

    const getAll = async (msg) => {
        try {
            //$("#studentList").text("Finding Student Information...");
            let accountID = sessionStorage.getItem("accountID")
            let permission = (sessionStorage.getItem("Permission"))


            //in case of ADmin account, we switch the number to 1 ONLY for searchign up  
            let account_ID_PURCHASES
            if (permission === "Administrator") {
                account_ID_PURCHASES = 1
            } else {
                account_ID_PURCHASES = accountID
            }

            let response = await fetch(`api/purchase/${account_ID_PURCHASES}`);
            if (response.ok) {
                let payload = await response.json(); // this returns a promise, so we await it
                buildPurchasesForm(payload);
                //msg === "" ? // are we appending to an existing message
                //    $("#status").text("Students Loaded") : $("#status").text(`${msg} - Students Loaded`);
            } else if (response.status !== 404) { // probably some other client side error
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);S
            } else { // else 404 not found
            } // else
        } catch (error) {
            console.log("error in getall")
            $("#status").text(error.message);
        }
    }; // getAll

    const buildPurchasesForm = (data, usealldata = true) => {
        $("#logsList").empty();
        let permission = (sessionStorage.getItem("Permission"))

        $("#buttonDiv").empty();
        if (permission === "Administrator") {
            $("#updateStatusDiv").empty();
            updateStatusDiv = $(` <div class="card-footer text-center">
                                            <label for="formStatus">Pending order</label>


                                            <select name="statusForForm" id="formStatusUpdate">
                                                <option></option>

                                                <option value="Approved">Approved</option>
                                                <option value="Denied">Denied</option>
                                            </select>
                                            <input type="button" class="btn btn-secondary" value="Update Status" id="actionbutton" />
                                            <div id="modalstatus" class="text-left mt-3 mb-2"></div>

                                        </div>`)
            //buttonDiv = $(`<button id="UpdateAccount" data-toggle="updateModal" data-target="updateModal">Recover Account</button>`)
            updateStatusDiv.appendTo("#updateStatusDiv")

            buttonDiv = $(`<button id="AddUser" data-toggle="addModal" data-target="addModal">Add Account</button>
                            <button id="UpdateAccount" data-toggle="myUpdateModal" data-target="myUpdateModal">Recover Account</button>`)
            //buttonDiv = $(`<button id="UpdateAccount" data-toggle="updateModal" data-target="updateModal">Recover Account</button>`)
            buttonDiv.appendTo("#buttonDiv")
        }
            
        buttonDiv = $(`
        <button id="CreateForm" data-toggle="myModal" data-target="myModal">Add Purchase</button>
        `)
        buttonDiv.appendTo("#buttonDiv")

        div = $(`

<div   class="list-group-item text-white bg-secondary row d-flex" id="status">Purchases</div>
              
<div class= "list-group-item row d-flex text-center" id="heading">
                <div class="col-2 h6">Supplier</div>
                <div class="col-2 h6">Buyer</div>
                <div class="col-2 h6">Net</div>
                <div class="col-2 h6">Date</div>
                <div class="col-2 h6">Status</div>
`);
        div.appendTo($("#logsList"));

        usealldata ? sessionStorage.setItem("allLogs", JSON.stringify(data)) : console.log("null??");
        data.forEach(async form => {
            let username
            let response = await fetch(`api/login/getUserByID/${form.accountID}`)
            if (response.ok) {
                let payload = await response.json(); // th
                username = payload.accountName
            }
            var formatDate = new Date((form.purchaseDate)); 
            var dd = String(formatDate.getDate()).padStart(2, '0');
            var mm = String(formatDate.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = formatDate.getFullYear();
            formatDate = mm + '/' + dd + '/' + yyyy;
           
            btn = $(`<button class="list-group-item row d-flex"  id="${form.purchase_ID}">`);
            btn.html(`<div class="col-2" id="logCompany${form.supplier}">${form.supplier}</div>
                        <div class="col-2" id="logDate${username}">${username}</div>
                        <div class="col-2" id="logLength${form.net}">$${form.net}</div>
                        <div class="col-2" id="logLength${formatDate}">${formatDate}</div>
                        <div class="col-2" id="logLength${form.status}">${form.status}</div>
                `
            );
            btn.appendTo($("#logsList"));
        }); // forEach
    }; // buildStudefirstntList

    $("#srch").keyup(() => {
        let alldata = JSON.parse(sessionStorage.getItem("allLogs"));
        let filtereddata = alldata.filter((stu) => stu.supplier.match(new RegExp($("#srch").val(), 'i')));
        buildPurchasesForm(filtereddata, false);
    }); 

    //work on searching for buyer and not only supplier. Still needs to work on it
    //$("#buyersrch").keyup(() => {
    //    let alldata = JSON.parse(sessionStorage.getItem("allLogs"));
    //    let filtereddata = alldata.filter((stu) => stu.supplier.match(new RegExp($("#srch").val(), 'i')));
    //    buildPurchasesForm(filtereddata, false);
    //}); 

    $("#userList").keyup(() => {
        updateTotal();

    }); // srch keyup

    const updateTotal = () =>{
        let quantity = $("#TextBoxQuantity").val();
        let productPrice = $("#TextBoxPrice").val();
        let tax = 1
        if (document.getElementById('includeTax').checked) {
            tax = 1.13
        }

        let totalValue = (quantity * productPrice) * tax
        $("#TextBoxTotalAfterTax").val(totalValue);

    }

    $("#userList").click(async (e) => {
       // clearModalFields();
        if (!e) e = window.event;
        let formID = e.target.parentNode.id;

        let data = JSON.parse(sessionStorage.getItem("allLogs"))
        data.forEach(async (log) => {
            //console.log("comparing " + log.formID +" and "+formID)
            let username;
            let buyer
            if (log.purchase_ID === parseInt(formID)) {
               sessionStorage.setItem("formID", formID)
                $("#logModal").modal("toggle");
                //let response = await fetch(`api/form/${accountID}`);
                let response = await fetch(`api/login/getUserByID/${log.accountID}`)
                if (response.ok) {
                    let payload = await response.json(); // th
                    username = payload.accountName
                }

                let response_purchase = await fetch(`api/Purchase/getByID/${log.purchase_ID}`)
                if (response_purchase.ok) {
                    let payload_purchase = await response_purchase.json(); // th
                    buyerID = payload_purchase.accountID_approver
                }

                let response_buyer = await fetch(`api/login/getUserByID/${buyerID}`)
                if (response_buyer.ok) {
                    let payload = await response_buyer.json(); // th
                    buyer = payload.accountName
                }

                let date = log.purchaseDate
                date = date.substr(0,10)

                $("#view_supplier").val(log.supplier)
                $("#view_buyer").val(username);
                $("#view_reference").val(log.reference);
                $("#view_quantity").val(log.quantity);
                $("#view_productPrice").val("$"+log.productPrice);
                $("#view_tax").val(log.tax);
                $("#view_net").val("$" +log.net);
                $("#view_total").val("$" +log.totalAfterTax);
                $("#view_status").val(log.status);
                $("#view_approvedBy").val(buyer);

                $("#view_supplier").attr('readonly', true);
                $("#view_buyer").attr('readonly', true);
                $("#view_reference").attr('readonly', true);
                $("#view_quantity").attr('readonly', true);
                $("#view_productPrice").attr('readonly', true);
                $("#view_tax").attr('readonly', true);
                $("#view_net").attr('readonly', true);
                $("#view_total").attr('readonly', true);
                $("#view_status").attr('readonly', true);
                $("#view_approvedBy").attr('readonly', true);
            } else {
                //console.log(".")
            }
            //console.log(log)fact
        })
    }); 

    const clearUpdateModal = () => {

      $("#Recover_TextBoxUsername").val("");
      $("#Recover_TextBoxUserPassword").val("");
      $("#Recover_TextBoxUserPassword_confirm").val("");
      $("#addUserStatus").text("");

    }
}); // jQuery re


