﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    public class Vocabulary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VocabularyId { get; set; }
        // 導覽屬性
        public Sentence Sentence { get; set; }
        // FK(attribute optional!)
        [ForeignKey("Sentence")]
        public int SentenceId { get; set; }

        // 這裡不加[Required]，因為如果有空的控制項沒用到，到時繫結會出錯
        [Column(TypeName = "nvarchar(100)")]
        public string Voc { get; set; }

        [Column(TypeName = "varchar(4)")]
        public string Category { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Chinese { get; set; }


    }
}
