$(function () {

    console.log("main.js");

    GetRolesData();

    $("#sidebarCollapse").on("click", function () {
        $("#sidebar").toggleClass("active");
    });

    ShowData();

    $("#newTicket").click(function () {
        $("#popUpNewTicket").removeAttr("hidden");
    });

    $("#cencelTicket").click(function () {
        $("#popUpNewTicket").attr("hidden", true);
    });

    $("#fileSelect").on("click", function (e) {
        e.preventDefault();
        setTimeout(function () {
            $("#formFileMultiple").click();
        }, 100);
    });

    $('#NewUserSave').on('click', function () {

        AddNewEmployee();
    });

    $('#NewUserCancel').on('click', function () {

        $("#addUserForm").attr("hidden", "hidden");
    });

    $('#addNewUser').on('click', function () {

        $("#addUserForm").removeAttr("hidden");
    });

});

function ShowData() {
    var clients = [
        {
            Expect_Prod: "28/03/2023",
            Work_time: "30",
            Ticket_Title: "יש להוסיף",
            Status: "Open",
            Opened_by: "sergey",
            Developer_Name: "sergey",
            Priority: "גבוהה",
            App: "CRM",
            Department: "App",
            Ver_Number: "1128",
            Ticket_Number: "7895412",
        },
        {
            Expect_Prod: "28/03/2023",
            Work_time: "30",
            Ticket_Title: "יש להוסיף",
            Status: "Open",
            Opened_by: "hofit",
            Developer_Name: "sergey",
            Priority: "נמוכה",
            App: "CRM",
            Department: "App",
            Ver_Number: "1128",
            Ticket_Number: "7895412",
        },
    ];

    $("#jsGrid").jsGrid({
        width: "100%",
        height: "400px",

        inserting: false,
        editing: false,
        sorting: true,
        paging: true,
        filtering: true,

        data: clients,

        fields: [
            {
                name: "Expect_Prod",
                title: "צפי עלייה לייצור",
                type: "text",
                width: 100,
            },
            { name: "Work_time", title: "זמן עבודה", type: "number", width: 80 },
            { name: "Ticket_Title", title: "כותרת קריאה", type: "text", width: 200 },
            {
                name: "Status",
                title: "סטטוס",
                type: "text",
                width: 90,

                itemTemplate: function (value, item) {
                    if (value == "Open") {
                        return $("<div>")
                            .css({
                                color: "green",
                                fontWeight: "bold"
                            })
                            .text(value);
                    }
                    else {
                        return value;
                    }
                },

            },
            { name: "Opened_by", title: "נפתח על ידי", type: "text", width: 120 },
            { name: "Developer_Name", title: "מפתח", type: "text", width: 100 },
            {
                name: "Priority",
                title: "עדיפות",
                type: "text",
                width: 100,

                itemTemplate: function (value, item) {
                    if (value == "גבוהה") {
                        return $("<div>")
                            .css({
                                "background-color": "#ed5252",
                                color: "white",
                                fontWeight: "bold"
                            })
                            .text(value);
                    }
                    else {
                        return value;
                    }
                },
            },
            { name: "App", title: "אפליקציה", type: "text", width: 100 },
            { name: "Department", title: "מחלקה", type: "text", width: 100 },
            { name: "Ver_Number", title: "מספר גירסה", type: "text", width: 80 },
            { name: "Ticket_Number", title: "מספר תקלה", type: "text", width: 80 },
        ],

        rowClick: function (args) {
            var rowData = args.item;
            var rowDataString = JSON.stringify(rowData);
            localStorage.setItem("Ticket_Ditails", rowDataString);
            OpenEditPage();
        },
    });
}
function OpenEditPage() {
    window.open("EditTicket.html", "_blank").focus();
}
function GetRolesData() {

    $.ajax({
        url: "/Main?handler=RolesData",
        type: "GET",
        async:true,
        success: function (data) {

            if (data) {

                var $select = $("#titleUser");
                $select.empty();
                $.each(data, function (index, item) {
                    $select.append($("<option>", {
                        value: item.role_id,
                        text: item.role_name
                    }));
                });
            }          
        },
        error: function (xhr, status, error) {
            console.error("Error fetching data:", error);
        }
    });

}
function AddNewEmployee(event) {

    event.preventDefault(); 
    const form = event.target;
    const formData = new FormData(form);

    fetch("/Main?handler=AddNewUser", {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                Swal.fire({
                    title: "!משתמש נוסף בהצלחה",
                    icon: "success"
                });
            } else {
                throw new Error(data.message);
            }
        })
        .catch(error => {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "!אירעה שגיאה בהוספת משתמש",
            });
        });

}
