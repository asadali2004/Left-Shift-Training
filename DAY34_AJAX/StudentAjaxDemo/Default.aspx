<%@ Page Language="C#" AutoEventWireup="true"
         CodeBehind="Default.aspx.cs"
         Inherits="StudentAjaxDemo._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Student Search — AJAX Demo</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; background: #f4f4f4; }

        .container  { max-width: 750px; margin: auto; background: white;
                      padding: 30px; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }

        h2          { color: #2c3e50; }

        input[type=number] { padding: 8px; width: 120px; font-size: 15px;
                             border: 1px solid #ccc; border-radius: 4px; }

        button      { padding: 9px 20px; font-size: 14px; border: none;
                      border-radius: 4px; cursor: pointer; margin-left: 8px; }

        .btn-search { background: #3498db; color: white; }
        .btn-all    { background: #27ae60; color: white; }
        .btn-clear  { background: #e74c3c; color: white; }

        button:hover { opacity: 0.85; }

        #spinner    { display: none; color: #888; margin-top: 12px; font-style: italic; }

        /* Single student result card */
        .student-card { background: #eaf4fb; border-left: 5px solid #3498db;
                        padding: 15px 20px; margin-top: 20px; border-radius: 4px; }
        .student-card h3 { margin: 0 0 10px; color: #2c3e50; }
        .student-card p  { margin: 4px 0; color: #555; }

        /* All students table */
        table  { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th     { background: #2c3e50; color: white; padding: 10px; text-align: left; }
        td     { padding: 9px 10px; border-bottom: 1px solid #ddd; }
        tr:hover td { background: #f0f8ff; }

        .error-msg  { color: #e74c3c; margin-top: 15px; font-weight: bold; }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <%-- ScriptManager: Required for AJAX in Web Forms --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="container">

        <h2>🎓 Student Search System</h2>
        <p style="color:#888">Enter a Student ID (1–5) and click Search, or load all students.</p>

        <%-- Search Controls --%>
        <input type="number" id="txtStudentId" placeholder="Student ID" min="1" max="99" />
        <button type="button" class="btn-search" id="btnSearch">🔍 Search</button>
        <button type="button" class="btn-all"    id="btnAll">📋 Show All</button>
        <button type="button" class="btn-clear"  id="btnClear">✖ Clear</button>

        <%-- Loading Spinner --%>
        <div id="spinner">⏳ Please wait...</div>

        <%-- Results appear here --%>
        <div id="divResult"></div>

    </div>

</form>

<%-- jQuery from CDN --%>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
    // ── Helper: show/hide spinner ────────────────────────────────
    function showSpinner()  { $('#spinner').show(); $('#divResult').html(''); }
    function hideSpinner()  { $('#spinner').hide(); }

    // ── Helper: disable/enable buttons during AJAX call ─────────
    function lockButtons()   { $('button').prop('disabled', true); }
    function unlockButtons() { $('button').prop('disabled', false); }


    // ── SEARCH by Student ID ─────────────────────────────────────
    $('#btnSearch').click(function () {

        var id = parseInt($('#txtStudentId').val());

        // Basic validation before calling server
        if (isNaN(id) || id <= 0) {
            $('#divResult').html('<p class="error-msg">⚠️ Please enter a valid Student ID.</p>');
            return;
        }

        showSpinner();
        lockButtons();

        $.ajax({
            type: 'POST',
            url: 'Default.aspx/SearchStudent',           // Points to WebMethod
            data: JSON.stringify({ studentId: id }),      // Send the ID
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (response) {
                hideSpinner();
                unlockButtons();

                // ASP.NET always wraps result in .d
                var result = response.d;

                if (result === 'NOT_FOUND') {
                    $('#divResult').html(
                        '<p class="error-msg">❌ No student found with ID ' + id + '.</p>'
                    );
                    return;
                }

                // Parse JSON string into a JavaScript object
                var s = JSON.parse(result);

                // Build a nice card from the data
                var html =
                    '<div class="student-card">' +
                    '  <h3>👤 ' + s.Name + '</h3>' +
                    '  <p><b>ID:</b> '     + s.Id     + '</p>' +
                    '  <p><b>Email:</b> '  + s.Email  + '</p>' +
                    '  <p><b>Course:</b> ' + s.Course + '</p>' +
                    '  <p><b>Age:</b> '    + s.Age    + '</p>' +
                    '</div>';

                $('#divResult').html(html);
            },

            error: function () {
                hideSpinner();
                unlockButtons();
                $('#divResult').html('<p class="error-msg">🔴 Server error. Please try again.</p>');
            }
        });
    });


    // ── SHOW ALL Students ────────────────────────────────────────
    $('#btnAll').click(function () {

        showSpinner();
        lockButtons();

        $.ajax({
            type: 'POST',
            url: 'Default.aspx/GetAllStudents',
            data: JSON.stringify({}),                     // No parameters needed
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (response) {
                hideSpinner();
                unlockButtons();

                var students = JSON.parse(response.d);    // Array of student objects

                // Build an HTML table
                var html =
                    '<table>' +
                    '  <tr><th>ID</th><th>Name</th><th>Email</th><th>Course</th><th>Age</th></tr>';

                $.each(students, function (index, s) {
                    html +=
                        '<tr>' +
                        '  <td>' + s.Id     + '</td>' +
                        '  <td>' + s.Name   + '</td>' +
                        '  <td>' + s.Email  + '</td>' +
                        '  <td>' + s.Course + '</td>' +
                        '  <td>' + s.Age    + '</td>' +
                        '</tr>';
                });

                html += '</table>';
                $('#divResult').html(html);
            },

            error: function () {
                hideSpinner();
                unlockButtons();
                $('#divResult').html('<p class="error-msg">🔴 Could not load students.</p>');
            }
        });
    });


    // ── CLEAR Results ────────────────────────────────────────────
    $('#btnClear').click(function () {
        $('#divResult').html('');
        $('#txtStudentId').val('');
    });


    // ── Allow pressing Enter key to search ───────────────────────
    $('#txtStudentId').keypress(function (e) {
        if (e.which === 13) {          // 13 = Enter key
            $('#btnSearch').click();
        }
    });
</script>

</body>
</html>