using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.Results
{
    /// <summary>
    /// Représente le résultat d'une opération pouvant réussir ou échouer sans valeur de retour.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Convertit implicitement une instance d'<see cref="Error"/> en un résultat d'échec.
        /// </summary>
        /// <param name="error">L'erreur survenue.</param>
        public static implicit operator Result(Error error)
        {
            return Failure(error);
        }

        /// <summary>
        /// Crée un résultat indiquant une opération réussie.
        /// </summary>
        /// <returns>Une instance de <see cref="Result"/> en succès.</returns>
        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        /// <summary>
        /// Crée un résultat indiquant une opération en échec avec l'erreur spécifiée.
        /// </summary>
        /// <param name="error">L'erreur ayant causé l'échec de l'opération.</param>
        /// <returns>Une instance de <see cref="Result"/> en échec.</returns>
        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }

    /// <summary>
    /// Représente le résultat d'une opération pouvant réussir ou échouer avec une valeur de retour typée.
    /// </summary>
    /// <typeparam name="TResult">Le type de données contenu en cas de succès.</typeparam>
    public class Result<TResult>
    {
        /// <summary>
        /// Convertit implicitement une instance d'<see cref="Error"/> en un résultat d'échec typé.
        /// </summary>
        /// <param name="error">L'erreur survenue.</param>
        public static implicit operator Result<TResult>(Error error)
        {
            return Failure(error);
        }

        /// <summary>
        /// Crée un résultat typé indiquant une opération réussie avec les données produites.
        /// </summary>
        /// <param name="data">La valeur résultant de l'opération.</param>
        /// <returns>Une instance de <see cref="Result{TResult}"/> en succès avec ses données.</returns>
        public static Result<TResult> Success(TResult data)
        {
            return new Result<TResult>(true, data, Error.None);
        }

        /// <summary>
        /// Crée un résultat typé indiquant une opération en échec avec l'erreur spécifiée.
        /// </summary>
        /// <param name="error">L'erreur ayant causé l'échec de l'opération.</param>
        /// <returns>Une instance de <see cref="Result{TResult}"/> en échec.</returns>
        public static Result<TResult> Failure(Error error)
        {
            return new Result<TResult>(false, default!, error);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public TResult Data { get; }

        private Result(bool isSuccess, TResult data, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }
    }
}
