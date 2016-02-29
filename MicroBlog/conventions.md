#Ideas
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


