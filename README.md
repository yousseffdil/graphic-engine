# Proyecto de Mi Motor Gráfico

Este proyecto es una implementación básica de un motor gráfico en C# utilizando Windows Forms. Permite crear una ventana en la que se dibuja un cuadrado que rota y colisiona con un plano.
## Diagrama de Clases

A continuación se muestra el diagrama de clases del proyecto:
<div align="center">
    <img src="./preview/preview.png">
</div>

## Cálculos realizados en los archivos

| Archivo           | Método/Función                       | Descripción                                                                                            |
|-------------------|--------------------------------------|--------------------------------------------------------------------------------------------------------|
| Ventana.cs        | Initialize()                         | Configura el entorno OpenGL y la matriz de proyección.                                                 |
| Ventana.cs        | Timer_Tick(state)                    | Actualiza la posición del cuadrado y verifica la colisión con el plano.                                 |
| Ventana.cs        | OnPaint(e)                           | Dibuja el plano y el cuadrado con la textura emisiva en la ventana.                                     |
| Square.cs         | Square(position, size, mass)         | Inicializa un cuadrado con una posición, tamaño y masa dada.                                            |
| Square.cs         | UpdatePosition()                      | Actualiza la posición del cuadrado en función de su velocidad y aceleración.                            |
| Square.cs         | ReverseDirection()                    | Invierte la dirección de la velocidad del cuadrado para simular un rebote.                              |
| Square.cs         | IsCollidingWith(plane)                | Verifica si el cuadrado colisiona con el plano.                                                         |
| Plane.cs          | Plane(position, width, height)        | Inicializa un plano con una posición, ancho y altura dada.                                              |
| OpenGLRenderer.cs | Initialize()                          | Configura OpenGL, habilita capacidades y configura la matriz de proyección y visualización.             |
| OpenGLRenderer.cs | LoadTexture(path)                     | Carga una textura emisiva desde un archivo de imagen y configura los parámetros de la textura en OpenGL. |
| OpenGLRenderer.cs | Render(square, plane)                 | Renderiza el cuadrado con la textura emisiva y el plano en OpenGL.                                      |


## Descripción del Proyecto

El proyecto consiste en una ventana que muestra un cuadrado que rota alrededor de su centro. El cuadrado tiene una velocidad inicial y una aceleración constante que simula una gravedad hacia abajo.

Si el cuadrado colisiona con el plano, se simula un rebote invirtiendo su dirección vertical.


## Requisitos del Proyecto

- Visual Studio 2019 o superior.
- .NET Framework 4.7.2 o superior.

##Informe de Errores - Proceso de Desarrollo

Durante el proceso de desarrollo del proyecto, se encontraron varios errores que afectaron el funcionamiento y la integración de las diferentes partes del sistema. A continuación, se detallan los errores más relevantes experimentados:

1. **Error en la carga de la textura emisiva**: Inicialmente, se intentó cargar la textura emisiva utilizando la clase `TextureBrush` de la biblioteca `System.Drawing`. Sin embargo, se generó una excepción `ArgumentException` con el mensaje "Parameter is not valid". Esto se debió a una incompatibilidad entre la ruta del archivo de la textura y la ubicación desde donde se estaba ejecutando la aplicación. La solución fue proporcionar la ruta absoluta del archivo de textura para asegurarse de que se encontrara correctamente.

2. **Error de referencia a la biblioteca OpenTK**: Al intentar utilizar la biblioteca OpenTK para trabajar con OpenGL, se encontró un error de referencia, especificamente "OpenTK not found". Esto se debe a que la biblioteca OpenTK no estaba instalada en el entorno de desarrollo. La solución fue instalar la biblioteca OpenTK a través de NuGet o mediante la descarga manual e incorporarla al proyecto.

3. **Error de compatibilidad entre OpenGL y OpenTK**: Una vez solucionado el error de referencia a OpenTK, se encontraron errores adicionales al intentar utilizar las clases y métodos de OpenGL a través de OpenTK. Esto se debe a diferencias de versiones y compatibilidad entre OpenGL y OpenTK. Para resolver estos errores, es necesario asegurarse de utilizar las versiones correctas y compatibles de OpenGL y OpenTK, y ajustar el código en consecuencia.

4. **Errores sintácticos y de lógica**: Durante la integración del código en diferentes archivos y clases, se encontraron errores sintácticos y de lógica que causaron un funcionamiento incorrecto o inesperado del sistema. Estos errores incluyen typos, mal uso de métodos y parámetros, falta de llamadas a métodos necesarios, entre otros. La solución consistió en revisar cuidadosamente el código, depurarlo paso a paso y corregir los errores encontrados.

5. **Errores de rendimiento**: En algunos casos, el rendimiento del sistema pudo verse afectado debido a la forma en que se realizaban los cálculos o las llamadas a OpenGL. Estos errores se manifestaron en una baja tasa de cuadros por segundo (FPS) o en una ejecución lenta de la aplicación. Para solucionar estos problemas, se requirió optimizar el código, revisar la lógica de los cálculos y evitar llamadas innecesarias a OpenGL.

En resumen, el desarrollo de este proyecto presentó desafíos relacionados con la carga de texturas, la integración de OpenGL a través de OpenTK y la corrección de errores sintácticos y de lógica. La resolución de estos errores requirió un enfoque de depuración sistemático, la instalación de bibliotecas adecuadas y la optimización del rendimiento. A medida que se solucionaron los errores, el sistema pudo funcionar correctamente y mostrar la textura emisiva en pantalla.
## Ejecución del Proyecto

1. Clona este repositorio en tu máquina local o descarga el código fuente.
    ```shell
    git clone https://github.com/yousseffdil/graphic-engine.git
2. Abre el proyecto en Visual Studio.
3. Compila y ejecuta el proyecto.
4. Se abrirá una ventana donde podrás ver el cuadrado rotando y colisionando con el plano.
