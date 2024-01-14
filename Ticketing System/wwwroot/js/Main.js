$(function () {

    ShowData();

    console.log("Main.JS");

    $("#sidebarCollapse").on("click", function () {
        $("#sidebar").toggleClass("active");
    });
    
    $("#newTicket").click(function () {
        $("#popUpNewTicket").removeAttr("hidden");
    });

    $("#cencelTicket").click(function () {
        $("#popUpNewTicket").attr("hidden", true);
    });

    $("#addNewUser").click(function () {
        $("#popUpNewUser").removeAttr("hidden");
    });
 
    $("#NewUserCancel").click(function () {
        $("#popUpNewUser").attr("hidden", true);
    });
    $("#changePassword").click(function () {
        $("#NewPassword").removeAttr("hidden");
    });

    

    $("#fileSelect").on("click", function (e) {
        e.preventDefault();
        setTimeout(function () {
            $("#formFileMultiple").click();
        }, 100);
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
            { name: "Status", title: "סטטוס", type: "text", width: 90 },
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
                                "background-color": "red",
                                color: "white",
                                "border-radius": "10px",
                            })
                            .text(value);
                    } else {
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
    window.open("/EditTicket", "_blank").focus();
}
