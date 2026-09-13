using System.Text.RegularExpressions;

namespace CrudUsuarios.Validaciones
{
    /// <summary>
    /// Regla de negocio AISLADA del CRUD de Usuarios.
    ///
    /// Esta es la unidad más pequeña que se somete a prueba unitaria manual:
    /// valida el formato del correo ANTES de insertar o actualizar un registro.
    ///
    /// Importante para el aislamiento de la unidad (Tarea 5):
    ///   - No depende de Entity Framework ni de ningún DbContext.
    ///   - No depende de la base de datos.
    ///   - No hace llamadas a APIs ni servicios externos.
    ///   - Es una función estática, pura y determinista: misma entrada, misma salida.
    /// </summary>
    public static class ValidadorUsuario
    {
        private static readonly Regex PatronCorreo =
            new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

        /// <summary>
        /// Valida el correo de un usuario.
        /// Lanza ArgumentException cuando el dato es inválido,
        /// y devuelve true cuando el correo es válido.
        /// </summary>
        public static bool ValidarCorreo(string? correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                throw new ArgumentException("El correo es obligatorio");
            }

            if (!PatronCorreo.IsMatch(correo))
            {
                throw new ArgumentException("Formato de correo inválido");
            }

            return true; // válido
        }
    }
}
