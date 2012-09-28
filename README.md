OnTime API Example
------------------

This repository contains two example applications that use the [OnTime RESTful API][api_docs].
One is "Defects Explorer", a C# Windows application allowing you to view, delete and add defects, as well as add attachments.
The other is "API Explorer", an MVC web application that allows you to make arbitrary OnTime API calls
and shows verbatim JSON responses.

Both examples are in a Visual Studio 2010 solution with the following projects:
 * WinApp - the Defects Explorer example
 * WebApp - the API Explorer example
 * OnTime - shared code used to communicate with the API

[api_docs]: http://developer.ontimenow.com

Download
--------
You can either [Download a ZIP file of this repository][download_zip] or clone the repository using git.

[download_zip]: http://github.com/Axosoft/OnTimeAPIExample/zipball/master

Running the Examples
--------------------
Follow the instructions on the [API documentation site][api_docs_getting_started] to obtain a Client ID
and Client Secret.  You will need to enter these along with your OnTime account URL in the projects' App.config
or Web.config files.

If you'd rather not work with your live OnTime data while experimenting with the API, you can sign up for a
trial account on http://www.ontimenow.com/.

[api_docs_getting_started]: http://developer.ontimenow.com/get-started