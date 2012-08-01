<script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.9/jquery.validate.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/mvc/3.0/jquery.validate.unobtrusive.min.js" type="text/javascript"></script>
<script>
    $(function () {
        $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('help-inline');
        });

        $('form').each(function () {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
            $(this).addClass('error');
            }
        });
        });
      

    });

    var page = function () {
        //Update that validator
        $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest(".control-group").addClass("error");
        },
        unhighlight: function (element) {
            $(element).closest(".control-group").removeClass("error");
        }
        });
    } ();
  
</script>