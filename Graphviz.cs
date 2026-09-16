using System.Diagnostics;
using System.IO;

namespace Practica2
{
    public static class GraphvizHelper
    {
        public static void GenerarGrafoCola(ColaReproduccion cola)
        {
            string pathDot = "cola.dot";
            using (StreamWriter sw = new StreamWriter(pathDot))
            {
                sw.WriteLine("digraph Cola {");
                sw.WriteLine("rankdir=LR;");
                sw.WriteLine("node [shape=record, style=filled, fillcolor=lightblue];");

                Nodo? actual = cola.ObtenerPrimero();
                int i = 0;
                
                while (actual != null)
                {
                    sw.WriteLine($"node{i} [label=\"{actual.Cancion.Titulo} | {actual.Cancion.Artista}\"];");
                    if (actual.Siguiente != null)
                    {
                        sw.WriteLine($"node{i} -> node{i + 1};");
                    }
                    actual = actual.Siguiente;
                    i++;
                }
                sw.WriteLine("}");
            }
            EjecutarComandoGraphviz("cola.dot", "cola.png");
        }

        public static void GenerarGrafoArbol(ArbolBinarioBusqueda arbol)
        {
            string pathDot = "arbol.dot";
            using (StreamWriter sw = new StreamWriter(pathDot))
            {
                sw.WriteLine("digraph Arbol {");
                sw.WriteLine("node [shape=circle, style=filled, fillcolor=lightgreen];");

                if (arbol.Raiz != null)
                {
                    EscribirNodosArbol(arbol.Raiz, sw);
                }
                sw.WriteLine("}");
            }
            EjecutarComandoGraphviz("arbol.dot", "arbol.png");
        }

        private static void EscribirNodosArbol(Nodo nodo, StreamWriter sw)
        {
            // Para que Graphviz reconozca nodos con espacios en el nombre, usamos comillas
            string nombreActual = $"\"{nodo.Cancion.Titulo}\"";

            if (nodo.Izquierdo != null)
            {
                string nombreIzquierdo = $"\"{nodo.Izquierdo.Cancion.Titulo}\"";
                sw.WriteLine($"{nombreActual} -> {nombreIzquierdo};");
                EscribirNodosArbol(nodo.Izquierdo, sw);
            }
            if (nodo.Derecho != null)
            {
                string nombreDerecho = $"\"{nodo.Derecho.Cancion.Titulo}\"";
                sw.WriteLine($"{nombreActual} -> {nombreDerecho};");
                EscribirNodosArbol(nodo.Derecho, sw);
            }
        }

        private static void EjecutarComandoGraphviz(string dotFile, string pngFile)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("dot", $"-Tpng {dotFile} -o {pngFile}")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(startInfo)!)
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Asegúrate de tener Graphviz instalado y en el PATH de Windows. Error: " + ex.Message);
            }
        }
    }
}