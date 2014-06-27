Axosoft API Example
------------------

This repository contains two example applications that use the [Axosoft RESTful API][api_docs].
One is "Defects Explorer", a C# Windows application allowing you to view, delete and add defects, as well as add attachments.
The other is "API Explorer", an MVC web application that allows you to make arbitrary Axosoft API calls
and shows verbatim JSON responses.

Both examples are in a Visual Studio 2010 solution with the following projects:
 * WinApp - the Defects Explorer example
 * WebApp - the API Explorer example

[api_docs]: http://developer.axosoft.com

Download
--------
You can either [Download a ZIP file of this repository][download_zip] or clone the repository using git.

[download_zip]: http://github.com/Axosoft/AxosoftAPIExample/zipball/master

Running the Examples
--------------------
Follow the instructions on the [API documentation site][api_docs_getting_started] to obtain a Client ID
and Client Secret.  You will need to enter these along with your Axosoft account URL in the projects' App.config
or Web.config files.

If you'd rather not work with your live Axosoft data while experimenting with the API, you can sign up for a
trial account on http://www.axosoft.com/.

[api_docs_getting_started]: http://developer.axosoft.com/getting-started

License
-------
Copyright 2012 Axosoft, LLC

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
