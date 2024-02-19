function EliminaAcentos(text) {
     text = text.replace(/[áàäâå]/, 'a');
     text = text.replace(/[éèëê]/, 'e');
     text = text.replace(/[íìïî]/, 'i');
     text = text.replace(/[óòöô]/, 'o');
     text = text.replace(/[úùüû]/, 'u');
     text = text.replace(/[ýÿ]/, 'y');

     text = text.replace(/[ÁÀÄÂÅ]/, 'A');
     text = text.replace(/[ÉÈËÊ]/, 'E');
     text = text.replace(/[ÍÌÏÎ]/, 'I');
     text = text.replace(/[ÓÒÖÔ]/, 'O');
     text = text.replace(/[ÚÙÜÛ]/, 'U');
     text = text.replace(/[ÝŸ]/, 'Y');
     return text;
}