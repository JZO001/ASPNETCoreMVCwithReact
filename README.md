# ASPNETCoreMVCwithReact
How to integrate ReactJS into ASP.NET Core MVC project with the ability to server-side rendering the HTML content

For detailed documentation, please visit: https://jzo.hu/blog/1002

To start the project for development, run "npm run start" in command prompt (don't forget to navigate to the ClientApp folder). For production optimized compilation, run "npm run build". Finally, in Visual Studio just simply run the project with IIS Express.

In the browser, you see the difference in the HTML HEAD, if you open three browser tab with these links:

https://localhost:44371/

https://localhost:44371/article/1

https://localhost:44371/article/2

Server render the initial HTML content (head, meta, scripts, styles, body, etc), sends it to the client in the response, than React starts. This solution is usefull,
if you would like to control the contents before the site rendered in the browser.
