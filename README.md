# Guía de instalación - Techreo Fullstack

## 1. Clonar el repositorio

Comienza por clonar el repositorio en tu entorno local:

git clone https://github.com/EmmanEsp/techreo-fullstack.git


## 2. Configuración de variables de entorno

Para configurar las variables de entorno, sigue estos pasos:

1. Ubica los archivos `.dist` en el directorio raíz del proyecto.
2. Renombra los archivos eliminando la extensión `.dist`. Por ejemplo:
   - Renombra `.env.backend.dist` a `.env.backend`
   - Renombra `.env.mongodb.dist` a `.env.mongodb`
   
**Nota:** No es necesario modificar el contenido de estos archivos a menos que desees personalizar la configuración.

## 3. Comandos Docker

Sigue estos pasos para construir y ejecutar los contenedores:

1. Construir la imagen
`docker compose build`

2. Levantar los contenedores en segundo plano
`docker compose up --detach`

3. Para detener y eliminar los contenedores
`docker compose down`

**Nota:** Si quieres volver a levantar los contenedores después de haberlos modificados, recomiendo usar --no-cache durante la creación de la imagen en el paso 1 y seguir con el paso 2.

## 3. Acceso a la aplicación

Abre tu navegador y ve a http://localhost:4200 para acceder a la aplicación.

### Registro de usuario

Dentro de la aplicación:

1. Haz clic en **"Crear Cuenta"** para registrarte.
2. Una vez creada la cuenta, podrás iniciar sesión y realizar depósitos y retiros desde tu cuenta.

### Pasos para descargar las imágenes de Docker Hub

1. **Descargar la imagen del backend:**

    ```bash
    docker pull emmanesp/fintech-backend:latest
    ```

2. **Descargar la imagen del frontend:**

    ```bash
    docker pull emmanesp/fintech-frontend:latest
    ```

3. **Crear una Docker network:**

    ```bash
    docker network create fintech-network
    ```

4. **Agregar MongoDB a la red para que el backend pueda conectarse:**

    ```bash
    docker run -d --network fintech-network --name fintech_mongodb mongo:6.0
    ```

5. **Ejecutar la imagen del backend con el siguiente comando:**

    ```bash
    docker run -d \
      --network fintech-network \
      -p 5001:8080 \
      --name fintech-backend \
      -e ASPNETCORE_ENVIRONMENT=Development \
      -e MongoDB__ConnectionString=mongodb://fintech_mongodb:27017/fintech \
      -e MongoDB__DatabaseName=fintech \
      -e JwtSettings__Secret=YourSuperSecretKeyThatIsAtLeast32CharactersLong \
      -e JwtSettings__Issuer=YourIssuer \
      -e JwtSettings__Audience=YourAudience \
      -e JwtSettings__ExpiryMinutes=60 \
      emmanesp/fintech-backend:latest
    ```

6. **Ejecutar la imagen del frontend con el siguiente comando:**

    ```bash
    docker run -d -p 4200:80 --name fintech-frontend emmanesp/fintech-frontend:latest
    ```
