using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace YueShanp.Models
{
    public class BaseEntity<T>
    {
        /// <summary>
        /// Model Id
        /// </summary>
        public virtual T Id { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        public virtual string Creator { get; set; }

        /// <summary>
        /// Create time.
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// Last editor.
        /// </summary>
        public virtual string LastEditor { get; set; }

        /// <summary>
        /// Last edit time.
        /// </summary>
        public virtual DateTime LastEditTime { get; set; }

        /// <summary>
        /// Model entity enable status.
        /// </summary>
        [ReadOnly(true)]
        public virtual EntityStatus EntityStatus { get; set; }
    }
}