namespace Practica2
{
    public class ColaReproduccion
    {
        private Nodo? primero;
        private Nodo? ultimo;

        public ColaReproduccion()
        {
            primero = null;
            ultimo = null;
        }

        public void Encolar(Cancion cancion)
        {
            Nodo nuevoNodo = new Nodo(cancion);

            if (primero == null)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                ultimo!.Siguiente = nuevoNodo;
                ultimo = nuevoNodo;
            }
        }

        public Cancion? Desencolar()
        {
            if (primero == null)
            {
                return null; // La cola está vacía
            }

            Cancion cancionReproducida = primero.Cancion;
            primero = primero.Siguiente;

            if (primero == null)
            {
                ultimo = null; // La cola ahora está vacía
            }

            return cancionReproducida;
        }

        // Calcula el tiempo total de la cola basado en la duración de cada canción en espera
        public int CalcularTiempoEstimado()
        {
            int tiempoTotal = 0;
            Nodo? actual = primero;

            while (actual != null)
            {
                tiempoTotal += actual.Cancion.Duracion;
                actual = actual.Siguiente;
            }

            return tiempoTotal;
        }

        public Nodo? ObtenerPrimero()
        {
            return primero;
        }
    }
}