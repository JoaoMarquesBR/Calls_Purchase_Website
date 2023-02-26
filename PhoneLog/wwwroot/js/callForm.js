



$(() => { // main jQuery routine - executes every on page load, $ is short for jquery

    $("#loginButton").click(async (e) => {
        var username = $("#usernameTF").val();
        var password = $("#passwordTF").val();
        //test for ignore
        //check if its valid
        try {
            let testBody;            
            let response = await fetch(`api/login/${username},${password}`, {
                method: "GET",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(testBody),
            });

            //console.log(testBody)

            let myData = await response.json();
            myData = JSON.stringify(myData);

            let getPermission = await fetch(`api/login/getUserByID/${myData}`)
            let permission = null
            if (getPermission.ok) {
                let payload = await getPermission.json(); // th
                permission = payload.groupPermission
                console.log("perm "+ permission)
            }
            sessionStorage.setItem("Permission", permission)
            $("#searchNav").show()

         if (myData >=0) {
             sessionStorage.setItem("accountID", myData)
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


    });

    $("#buttonDiv").click((e) => {
        let buttonClicked = e.target.id
        console.log(buttonClicked)
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
        var name = $("#TextBoxName").val();

        var password = $("#TextBoxUserPassword").val();

        var permissionGroup = $("#accountAccess").val();
        
        //check if its valid
        try {

            //check if username is in use
            //console.log("check username use ffirst")
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
                    $("#status").text("Account  " + username + " was inserted in the system")
                    $("#addModal").modal("toggle");
                    $("#TextBoxName").val("")
                    $("#TextBoxUsername").val("");
                    $("#TextBoxUserPassword").val("");
                    $("#addUserStatus").text("");
                }

            } 
        }catch (error) {
                // catastrophic
                console.log("error " + error)
                //console.log("DID NOT OCCUR ")
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
                    //console.log("password is too short")
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

        myForm = new Object();
        myForm.accountID = accountID;
        myForm.formID = -1;
        myForm.companyName = $("#TextBoxCompany").val();
        myForm.repName = $("#TextBoxRep").val();
        myForm.callLength = $("#TextBoxLength").val();
        myForm.callDesc = $("#TextBoxNote").val();

   
        let checkBoxNotMaked = true;
        if (document.getElementById('issueSolved').checked) {

            myForm.issueSolved = "Yes";
        } else if (document.getElementById('issueNotSolved').checked) {
            myForm.issueSolved = "No";
        } else {
            checkBoxNotMaked = false;
        }


        if (document.getElementById('NeedFollowUp').checked) {
            myForm.followUp = "Yes";
        } else if (document.getElementById('NoNeedFollowUp').checked) {
            myForm.followUp = "No";
        } else {
            checkBoxNotMaked = false;
        }



        myForm.callDate = new Date().toJSON;

        //console.log("Sending " + JSON.stringify(myForm))

        //console.log(checkBoxNotMaked)

        if (checkBoxNotMaked==false) {

        } else {


          let response = await fetch("api/form", {
                    method: "post",
                    headers: {
                        "content-type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify(myForm)
          });
            //console.log("added")
           $("#myModal").modal("toggle");
            getAll("");

        }
    });

    $("#NeedFollowUp").click(() => {
        $("#NoNeedFollowUp").prop("checked", false);
    });

    $("#NoNeedFollowUp").click(() => {
        $("#NeedFollowUp").prop("checked", false);
    })


    $("#issueSolved").click(() => {
        $("#issueNotSolved").prop("checked", false);

    });

    $("#issueNotSolved").click(() => {
        $("#issueSolved").prop("checked", false);
    })

    const getAll = async (msg) => {
        try {
            //$("#studentList").text("Finding Student Information...");
            let accountID = sessionStorage.getItem("accountID")
            let permission = (sessionStorage.getItem("Permission"))
            let account_ID_PURCHASES
            if (permission === "Administrator") {
                account_ID_PURCHASES = 1
            } else {
                account_ID_PURCHASES = accountID
            }
            let response = await fetch(`api/form/${account_ID_PURCHASES}`);
            if (response.ok) {
                let payload = await response.json(); // this returns a promise, so we await it
                buildStudentList(payload);
                //msg === "" ? // are we appending to an existing message
                //    $("#status").text("Students Loaded") : $("#status").text(`${msg} - Students Loaded`);
            } else if (response.status !== 404) { // probably some other client side error
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else { // else 404 not found
            } // else
        } catch (error) {
            console.log("error in getall")
            $("#status").text(error.message);
        }
    }; // getAll


    const buildStudentList = (data, usealldata = true) => {
        $("#logsList").empty();
        let permission = (sessionStorage.getItem("Permission"))

        $("#buttonDiv").empty();

        if (permission === "Administrator") {
            buttonDiv = $(`<button id="AddUser" data-toggle="addModal" data-target="addModal">Add Account</button>
                            <button id="UpdateAccount" data-toggle="myUpdateModal" data-target="myUpdateModal">Recover Account</button>`)
            //buttonDiv = $(`<button id="UpdateAccount" data-toggle="updateModal" data-target="updateModal">Recover Account</button>`)

            buttonDiv.appendTo("#buttonDiv")
        }
            

        buttonDiv = $(`
        <button id="CreateForm" data-toggle="myModal"  data-target="myModal">Add Call Log</button>
        `)
        buttonDiv.appendTo("#buttonDiv")

        div = $(`

          
<div   class="list-group-item text-white bg-secondary row d-flex" style="background-color: #21201e"  id="status">Your Call Logs</div>
              
<div class= "list-group-item row d-flex text-center" id="heading">
                <div class="col-2 h6">Company</div>
                <div class="col-2 h6">Date</div>
                <div class="col-2 h6">Call Length</div>
                <div class="col-2 h6">Needs follow up</div>
                <div class="col-2 h6">Solved</div>
                <div class="col-1 h6">Employee</div>
                <div class="col-1 h6">Case Number</div>


`);
        div.appendTo($("#logsList"));

        usealldata ? sessionStorage.setItem("allLogs", JSON.stringify(data)) : console.log("");

        //sessionStorage.setItem("allLogs", JSON.stringify(data));
        data.forEach(async form => {
            var formatDate = new Date((form.callDate)); 


            var dd = String(formatDate.getDate()).padStart(2, '0');
            var mm = String(formatDate.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = formatDate.getFullYear();
            formatDate = mm + '/' + dd + '/' + yyyy;

            let username
            let response = await fetch(`api/login/getUserByID/${form.accountID}`)
            if (response.ok) {
                let payload = await response.json(); // th
                username = payload.accountName
            }

            btn = $(`<button class="list-group-item row d-flex" id="${form.formID}">`);
            btn.html(`<div class="col-2 " id="logCompany${form.companyName}">${form.companyName}</div>
                        <div class="col-2" id="logDate${formatDate}">${formatDate}</div>
                        <div class="col-2" id="logLength${form.callLength}">${form.callLength}</div>
                        <div class="col-2" id="logFollowUp${form.followUp}">${form.followUp}</div>
                        <div class="col-2" id="logIssue${form.issueSolved}">${form.issueSolved}</div>
                        <div class="col-1" id="logIssue${username}">${username}</div>
                        <div class="col-1" id="logIssue${form.formID}">${form.formID}</div>
                `
            );
            btn.appendTo($("#logsList"));
        }); // forEach



        //<div class="col-4 h4">Call Time</div>

        //<div class="col-5" id="logTime${form.timeOfCall}">${form.timeOfCall}</div>

    }; // buildStudefirstntList

    $("#cardButton").click(async (e) => {
        let formID = sessionStorage.getItem("OpenLog")
        let newDesc = $("#view_description").val()
        let response 

        let responseUpdate = await fetch(`api/form/${newDesc},${formID}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(response),
        });
        if(responseUpdate.ok){
            let data = await responseUpdate.json();
            if (data == 1) {
                window.alert("Description has been updated")
            } else {
                window.alert("There were no changes to be made")
            }
        }

    })

    $("#srch").keyup(() => {
        let alldata = JSON.parse(sessionStorage.getItem("allLogs"));
        let filtereddata = alldata.filter((stu) => stu.companyName.match(new RegExp($("#srch").val(), 'i')));
        buildStudentList(filtereddata, false);
    }); // srch keyup

    $("#userList").click(async (e) => {
       // clearModalFields();
        
        if (!e) e = window.event;
        let formID = e.target.parentNode.id;

        if (formID !== "status") {
            //formID = e.target.id;
        } // clicked on row somewhere else

        //$("#logModal").modal("toggle");

        let data = JSON.parse(sessionStorage.getItem("allLogs"))
        data.forEach(async (log) => {
            //console.log("comparing " + log.formID +" and "+formID)
            let username;
            if (log.formID === parseInt(formID)) {
                $("#logModal").modal("toggle");
                //let response = await fetch(`api/form/${accountID}`);
                let response = await fetch(`api/login/getUserByID/${log.accountID}`)
                if (response.ok) {
                    let payload = await response.json(); // th
                    username = payload.accountName
                }


                let date = log.callDate
                date = date.substr(0,10)


                $("#view_description").val(log.callDesc)
                $("#view_companyName").val(log.companyName);
                $("#view_repName").val(log.repName);
                $("#view_empName").val(username);
                $("#view_callDate").val(date);
                $("#view_callLength").val(log.callLength);
                $("#view_followUp").val(log.followUp);
                $("#view_issueSolved").val(log.issueSolved);


                let accountID = sessionStorage.getItem("accountID");
                if (accountID != log.accountID) {
                    $("#view_description").attr('readonly', false);
                } else {
                    $("#cardButton").empty();
                    buttonDiv = $(`<button id="updateDesc">Update</button>`)
                    sessionStorage.setItem("OpenLog",log.formID)
                    buttonDiv.appendTo("#cardButton")
                }

                $("#view_companyName").attr('readonly', true);
                $("#view_repName").attr('readonly', true);
                $("#view_empName").attr('readonly', true);
                $("#view_callDate").attr('readonly', true);
                $("#view_callLength").attr('readonly', true);
                $("#view_followUp").attr('readonly', true);
                $("#view_issueSolved").attr('readonly', true);


            } else {
            //    console.log(".")
            }

            //console.log(log)
        })


    }); 

    const clearUpdateModal = () => {

      $("#Recover_TextBoxUsername").val("");
      $("#Recover_TextBoxUserPassword").val("");
      $("#Recover_TextBoxUserPassword_confirm").val("");
      $("#addUserStatus").text("");

    }


}); // jQuery re


