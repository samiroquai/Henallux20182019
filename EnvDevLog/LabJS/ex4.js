class Text {
  constructor(inputText) {
    this.words = inputText ? this.inputText.split(" ") : "";
  }

  capitalizeWord(word) {
    const firstLetter = word.charAt(0);
    const remainingLetters = word.substring(1);
    return firstLetter.toUpperCase() + remainingLetters;
  };

  capitalize() {
    const result = [];
    for (let word of this.words) {
      result.push(this.capitalizeWord(word));
    }
    return result.join(" ");
  };
}

function formatTextOO() {
  const inputText = prompt("Introduce your text to transform");
  const myText = new Text(inputText);
  const formattedText = myText.capitalize();
  alert(formattedText);
};