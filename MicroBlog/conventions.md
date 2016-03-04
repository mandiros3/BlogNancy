#Technical Ideas
Pay Attention to interfaces

+ Explore alternatives to razor: angular, vue, riot, ember, react.
+ Separate the UI from the service. Make it a true API, or do content
negotiation



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
revel all 3 menus, in addition show a logout text somewhere.

Replace login text with logout

```java 
 if(Login == true)
 {
    Menu[2].text("logout");
    Login.View(0) // hide the login form, alternatively just another view altogether
 }

```


