// ------------------code xử lý .ajax-link và form.ajax-form cập nhật động #main-content thông qua ajax------------
function loadContent(url) {
    console.log(url);
    $.get(url, function (result) {
        $('#main-content').html(result);
        // Re-parse validation for dynamically added content
        $.validator.unobtrusive.parse('#main-content');
    });
}

function LoadListTitle(url) {
    $.get(url, function (result) {
        $('#titleOfList').text('Danh sách cán bộ, nhân viên: ' + result);
    });
}

//For Detail, Edit, Delete link with .ajax-link
$(document).on('click', '.ajax-link', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');   
    console.log(url);
    loadContent(url);
});
 //Handle AJAX form submission
$(document).on('submit', 'form.ajax-form', function (e) {
    e.preventDefault();
    var $form = $(this);
    console.log("ValidationMessage");
    // Check if form is valid before submitting
    if (!$form.valid()) {
        return false;
    }

    var postUrl = $form.attr('action');
    var reloadUrl = $form.data('reload-url'); // custom attribute for reload

    $.post(postUrl, $form.serialize(), function () {
        console.log("Load content from: "+ reloadUrl);
        loadContent(reloadUrl); // pass the correct reload URL

        // Close the Bootstrap modal (assuming it's #editModal)
        $('.modal').modal('hide');
    });
    //loadContent(''); Nếu gọi ở đây thì cũng OK nhưng tốc độ chậm hơn so với khi
    // đặt trong $.post()
    //Hàm post thực thi submit. Do là hàm dùng chung, nếu chỉ trả về trang index thì gọi loadContent.



    
});


//If user navigates back via browser's "Back" button, you may want to refresh the active state:
window.addEventListener('pageshow', function () {
    $(document).ready(); // Rerun active menu restore
});

$(document).ready(function () {      
    console.log('ready in AjaxAdminLayout');
   //kiểm tra nếu menu-link nào có href trùng với url hiện tại thì active link đó, deactive các link còn lại, mở collapse
    const currentUrl = window.location.pathname;
    var activeLink;
    $('.menu-link').each(function () {
        if (currentUrl == $(this).attr("href")) {
            $(this).addClass('active');
            activeLink = $(this);
        }
        else
            $(this).removeClass('active');
    });
    
    if (activeLink) {
        const openCollapse = activeLink.closest('.accordion-collapse').attr('id');
        if (openCollapse) {
            const collapseElem = document.getElementById(openCollapse);
            if (collapseElem && !collapseElem.classList.contains('show')) {
                new bootstrap.Collapse(collapseElem, { toggle: true });                
            }
        }
    }
})

