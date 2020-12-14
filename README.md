# Udelar Online!  :boom: :rocket: :zap:

El repo consta de todo el código que se desarrolló del lado del Backend con .Net Core!
Esta solución fue generado con [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.1) versión  3.1.8.

  

## Antes arrancar..

  

Ejecutar el comando `dotnet restore` en la raíz de la solución.

  

## Luego... Servidor de desarrollo

  

Ubicarse en **UdelarOnline2020/WebAPI** y ejecutar `dotnet watch run`. A su vez, ubicarse dentro de **BedeliasOnline2020** y ejecutar `dotnet watch run` para tener levantados el core de la solución Backend, y  el periférico de Bedelias.
  

## Composición de la solución: UdelarOnline2020

  

La solución **UdelarOnline2020**  está compuesta por los siguientes proyectos:

 1. ***Business***:  En este proyecto se maneja toda la lógica de negocio de la aplicación
 2. ***Models***: Cómo su nombre lo indica, aquí podremos ver los diferentes modelos y clases que constituyen la aplicación.
 3. ***Perifericos***: Dentro de este proyecto, se encuentra la lógica de las notificaciones (Mails & PushNotifications) así como también la integración con el periferico de Bedelías.
 4.  ***Persistence***:  Aquí encontraremos el contexto (donde se detalla las diferentes relaciones entre las clases) de nuestra solución así como la información de las diferentes migraciones realizadas a lo largo de la implementación.
 5. ***Seguridad***: En este proyecto, se encuentra la implementación de los [JWT](https://jwt.io/) y la validación de los mismos.
 6. ***WebAPI***: Dentro de este proyecto podremos encontrar las diferentes configuraciones de la solucion así como también los diferentes controllers que exponen los servicios implementados.

