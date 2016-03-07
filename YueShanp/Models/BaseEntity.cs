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
        [HiddenInput(DisplayValue = false)]
        public virtual string Creator { get; set; }

        /// <summary>
        /// Create time.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// Last editor.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public virtual string LastEditor { get; set; }

        /// <summary>
        /// Last edit time.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime LastEditTime { get; set; }

        /// <summary>
        /// Model entity enable status.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public virtual EntityStatus EntityStatus { get; set; }
    }
}