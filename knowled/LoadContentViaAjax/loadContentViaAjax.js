/* i want to use ajax to render a partial view to the main-content tag 
when clicking on button */
/* To load a partial view into a div with AJAX in ASP.NET MVC5,
 follow this step-by-step guide. It assumes:
-You have a div with ID main-content.
-You click a button to load partial content from the server. */

/* ✅ 1. HTML Layout (View)
<!-- Main layout or a view file -->
<div id="main-content">
    <!-- AJAX content will be loaded here -->
</div>

<!-- Button to load partial -->
<button id="load-partial-btn" data-url="@Url.Action("MyPartial", "Home")">
    Load Partial View
</button> */

/* ✅ 2. Partial View (Example: _MyPartial.cshtml)
@model YourProject.Models.MyModel

<div>
    <h3>This is a partial view</h3>
    <p>Some dynamic content: @Model.SomeProperty</p>
</div> */

/* ✅ 3. Controller (HomeController.cs) */
/* public class HomeController : Controller
{
    public ActionResult MyPartial()
    {
        var model = new MyModel { SomeProperty = "Hello from server" };
        return PartialView("_MyPartial", model);
    }
} */

/* ✅ 4. AJAX Script using jQuery */
/* @section scripts {
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script> */

    $(document).ready(function () {
        $('#load-partial-btn').click(function () {
            var url = $(this).data('url');

            $.ajax({
                url: url,
                type: 'GET',
                success: function (result) {
                    $('#main-content').html(result);
                },
                error: function () {
                    alert('Failed to load content');
                }
            });
        });
    });

/* </script>
}
 */
/* ✅ Notes:

Ensure _MyPartial.cshtml is inside Views/Home/ folder.

No layout should be rendered with partial views, so make sure to not include _Layout.cshtml in the partial.

The controller should return PartialView(...), not View(...). */
////---------------------------------------------------------------------------/////

/* ok. and i need more operators with model like: detail, 
edit, delete by ajax. All in main-content tag */

/* Great! Here's a complete example of how to use AJAX to perform CRUD (Detail, Edit, Delete) 
operations and render results into the #main-content tag using ASP.NET MVC5. */

/* ✅ 1. Folder & File Structure

Controllers/

    HomeController.cs

Models/

    UserModel.cs

Views/Home/

    Index.cshtml

    _UserList.cshtml

    _UserDetail.cshtml

    _UserEdit.cshtml

Layout

    _Layout.cshtml (your shared layout) */
