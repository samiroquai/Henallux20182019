const capitalize = word => {
    const firstLetter = word.charAt(0);
    const remainingLetters = word.substring(1);
    return firstLetter.toUpperCase() + remainingLetters;
};

const capitalizeText = (text, capitalizeFn) => {
  return text.split(" ").map(capitalizeFn).join(" ");
};

function formatTextFunctional() {
  const inputText = prompt("Introduce your text to transform");
  const formattedText = capitalizeText(inputText, capitalize);
  alert(formattedText);
};