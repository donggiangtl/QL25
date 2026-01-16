$(document).ready(function () {
    console.log("Document Ready!"); // Debugging Check       
    

    // code kiểm tra tài khoản admin
    $('#mainForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission        
        $("#errorMsg").text("");
        $("#passwordInput").val("");
    });
    
    $("#verifyPasswordBtn").click(function () {
        
        var enteredPassword = $("#passwordInput").val();
        console.log("Entered Password: " + enteredPassword); // Debugging Check

        if (enteredPassword.length === 0) {
            $('#errorMsg').text('Password is required.');
            return;
        }                
        $.ajax({
            url: 'VerifyPassword',
            type: 'POST',
            data: { password: enteredPassword },
            success: function (response) {
                if (response.isValid) {
                    alert("Password verified successfully!");
                    
                    $("#passwordModal").modal('hide');
                    $('#mainForm').unbind('submit').submit(); // Submit the main form
                } else {
                    $("#errorMsg").text("Incorrect password!");
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error: ", error);
                $('#errorMsg').text('An error occurred while verifying the password.');
            }
        });
    });
   
    $("#EnviromentVariable").change(function () {
        //alert($("#DataSourceList").val());
        $("#DataSource").val($("#EnviromentVariable").val());
    });
    //SettingShow
    $('#componentsCollapse_1').on('shown.bs.collapse', function () {
        $('input[name="CollapseExpanded"]').val('expanded');
    });

    $('#componentsCollapse_1').on('hidden.bs.collapse', function () {
        $('input[name="CollapseExpanded"]').val('collapsed');
    });
})