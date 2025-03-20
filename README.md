
# 📌 Adquisiciones - Sistema de Gestión de Compras

Este proyecto consta de dos aplicaciones:
1. **AdquisicionesAPI**: API desarrollada en **.NET Core** con **Entity Framework**.
2. **adquisiciones-app**: Frontend desarrollado en **Angular 19** con **DaisyUI y Tailwind CSS**.

---

## 📌 Clonación
```sh
git clone https://github.com/bdsanchezc/adres.git
cd adquisiciones
```

## 🚀 Instalación y ejecución

### 🔹 1. Configuración de la API (`AdquisicionesAPI`)
#### **📌 Prerrequisitos**
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [SQLite (opcional, si deseas administrar la BD visualmente con DBeaver)](https://dbeaver.io/)

#### **📌 Instalación**
```sh
cd AdquisicionesAPI
dotnet restore
```

#### **⚙️ Configurar la Base de Datos**
Ejecuta los siguientes comandos para crear y aplicar las migraciones:
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### **▶️ Ejecución**
```sh
dotnet run
```
 🔹 La API estará disponible en: http://localhost:5230/swagger (Swagger UI)

### 🔹 2. Configuración del Frontend (adquisiciones-app)
#### **📌 Prerrequisitos**
- [Node.js (18+)](https://nodejs.org/es)
- [Angular CLI (si no lo tienes instalado, usa npm install -g @angular/cli)](https://dbeaver.io/)

#### **📌 Instalación**
```sh
cd adquisiciones-app
npm install
```

#### **▶️ Ejecución**
```sh
ng serve
```
🔹 El frontend estará disponible en: http://localhost:4200/

## 📚 Endpoints principales
#### **✅ Adquisiciones**

| Método | Endpoint | Descripción |
| :---|:---|:--- |
| `GET`  | `/api/Adquisiciones`     | Obtener todas las adquisiciones |
| `GET`  | `/api/Adquisiciones/{id}`     | Obtener una adquisición por ID |
| `POST`  | `/api/Adquisiciones`     | Crear una nueva adquisición |
| `PUT`  | `/api/Adquisiciones/{id}`     | Actualizar una adquisición |

#### **✅ Historial**
| Método | Endpoint | Descripción |
| :---|:---|:--- |
| `GET`  | `/api/HistorialAdquisiciones/{id}`     | Obtener el historial de una adquisicióon |
| `POST`  | `/api/HistorialAdquisiciones`     | Registrar un historial nuevo |

#### **✅ Parámetros**

| Método | Endpoint | Descripción |
| :---|:---|:--- |
| `GET`  | `/api/Parametricas/Estados`     | Obtener todos los estados para las adquisiciones |
| `GET`  | `/api/Parametricas/Unidades`     | Obtener la administrativa responsable |
| `GET`  | `/Parametricas/Proveedores`     | Obtener todas las entidades proveedoras |
