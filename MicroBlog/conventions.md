#Brainstorming
Pay Attention to interfaces
Make sure I sanitize input
Form validation also.
Add units tests, look into nancy browser
Add confirmation for both: update and delete
check out: https://github.com/MoonStorm/Dapper.FastCRUD

#Dependencies/ Libraries
Special note about PUT/DELETE requests: y default IIS 6 does not support PUT and DELETE verbs.
refer to: https://github.com/NancyFx/Nancy/wiki/Hosting-Nancy-with-asp.net

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


