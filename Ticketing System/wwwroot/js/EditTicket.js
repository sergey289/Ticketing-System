$(function () {

    console.log("EditTicket.JS");

    LoadData();
    
    $("#belongsVer").click(function () {

        $("#popUpVersionAssociation").removeAttr("hidden");

    });
    $("#VersionAssociationCancel").click(function () {

        $("#popUpVersionAssociation").attr("hidden", true);
    });
    
    $("#btn-belonghoursReport").click(function () {

        $("#popUpHoursReport").removeAttr("hidden");

    });
    $("#hoursReportCancel").click(function () {

        $("#popUpHoursReport").attr("hidden", true);
    });
    

});

function InitializePage() { }

function LoadData() {
    var rowDataString = localStorage.getItem("Ticket_Ditails");

    if (rowDataString) {
        var rowData = JSON.parse(rowDataString);
        $("#ticketDescription").val(rowData.Opened_by);
        localStorage.removeItem("Ticket_Ditail");
    }
}
