using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal.Entities
{
    /// <summary>
    /// Сущность задачи
    /// </summary>
    [Table("tasks")]
    public sealed class TaskEntity
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        [Column("title")]
        [MaxLength(500)]
        public string? Title { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего задачу (внешний ключ)
        /// </summary>
        [Column("created_by_user_id")]
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Дата и время создания задачи (UTC)
        /// </summary>
        [Column("created_utc")]
        public DateTimeOffset CreatedUtc { get; set; }

        /// <summary>
        /// Навигационное свойство к пользователю
        /// </summary>
        [ForeignKey(nameof(CreatedByUserId))]
        public User? CreatedByUser { get; set; }
    }
}
