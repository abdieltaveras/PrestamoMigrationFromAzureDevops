var language;
if (window.navigator.languages)
{ language = window.navigator.languages[0]; }
else
{ language = window.navigator.language || window.navigator.language; }
console.log("language ",language);