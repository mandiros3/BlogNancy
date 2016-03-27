#Technical Ideas
+ Pay Attention to interfaces
+ Make sure I sanitize input
+ Form validation also.
+ Add units tests, look into nancy browser
+ Add confirmation for both: update and delete
+ check out: https://github.com/MoonStorm/Dapper.FastCRUD
+ Maybe show top 10 posts and and show all button
+ Prevent view deletion if no connectivity to network. Update view, only if there was
a response

#Dependencies/ Libraries
Special note about PUT/DELETE requests: y default IIS 6 does not support PUT and DELETE verbs.
refer to: https://github.com/NancyFx/Nancy/wiki/Hosting-Nancy-with-asp.net

##Useful link :
+ Got inspired by: https://github.com/leegould/MicroBlog (main difference is mine doesn't use an ORM because I wanted to learn)
+ ASP.NET Version http://www.asp.net/web-api/overview/older-versions/creating-a-web-api-that-supports-crud-operations
+ Explore alternatives to razor: angular, vue, riot, ember, react.
+ Separate the UI from the service. Make it a true API, or do content
negotiation
+ Remember to enable http verbs put and delete in IIS : http://stevemichelotti.com/resolve-404-in-iis-express-for-put-and-delete-verbs/



#General Ideas
*******
+ Show only the title of the posts, onclick show everything.
+ Another way, show some of the text then open everything onclick
+ Allow to edit on login only
+ Add admin panel.
+Show number of posts, search ext....

##Useful link :
 https://github.com/jmk22/todo_categories_databases_CRUD/blob/master/Modules/HomeModule.cs
Great for modules, list many possible scenarios
*******
+Make it more modular, try to separate view from model more
+Possibly extend this into a mini .net CMS
+Add Custmomization Options


*******
#Menu
  
Read Write Login


Hide menu and read, show only login page, upon login
reveal all 3 menus, in addition show a logout text somewhere.

Replace login text with logout

```java 
 if(Login == true)
 {
    Menu[2].text("logout");
    Login.View(0) // hide the login form, alternatively just another view altogether
 }

```


