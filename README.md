# 📘 Sistema de Pólizas

Aplicación full-stack para la gestión de pólizas, con backend en **.NET 9**, frontend en **Angular 19** y base de datos en **SQL Server**. Puede ejecutarse vía Docker o de forma manual.

---

## 🧰 Requisitos

- Docker / Docker Desktop  
- (Opcional) [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)  
- (Opcional) [Node.js v20.11.1+](https://nodejs.org/)  
- (Opcional) Angular CLI 19+ → `npm install -g @angular/cli`

---

## 🚀 Ejecución con Docker

Desde la raíz del proyecto, ejecuta:

```bash
docker-compose up --build -d
```

Una vez levantado, abre en el navegador:

- Frontend: [http://localhost:8101/login](http://localhost:8101/login)  
- Backend (Swagger): [http://localhost:44319/swagger/index.html](http://localhost:44319/swagger/index.html)

---

## 🔧 Ejecución Manual (opcional)

### 🖥 Backend (.NET 9)

- Abre la solución [.sln](polizasBack/Policies.sln) en Visual Studio o desde CLI.
- Asegúrate de que el string de conexión apunte a una instancia de SQL Server válida.

### 🗄 Base de Datos

- Ejecuta el script de inicialización: [queries.sql](polizasBack/sql/queries.sql)

### 🌐 Frontend (Angular 19)

```bash
cd polizasFront
npm install
ng serve
```

> Asegúrate de que el backend esté corriendo en el puerto correcto. Verifica y ajusta el archivo de entorno:  
> [environment.ts](polizasFront/src/environments/environment.ts)

---

## 🗂 Estructura del Proyecto

```
├── docker-compose.yml
├── polizasBack/         # Proyecto .NET (API + SQL)
│   ├── Policies.sln
│   └── sql/
│       └── queries.sql
├── polizasFront/        # Proyecto Angular
│   ├── src/
│   │   └── environments/environment.ts
│   └── ...
```
