// This function is just a normal callback that can be transform into a promise
function createProcessLinePromise(line) {
    return processLine(line).then(words => {
      const capitalizeWords = words.forEach(word => {
          capitalize(word);
      });
      return capitalizeWords.join("");
    });
  };
  
  function capitalizeText(file) {
    // The first two callback functions are converted to a promise
    readFile(file)
       // This function take the argument from the first promise and send it back to the next
      .then(lines => removeMarks(lines))
      .then(lines => {
          // We build an array of promises for each value of lines
          const promisesArray = lines.forEach(createProcessLinePromise(line));
          return Promise.all[promisesArray];
      })
      .catch(error => {
          throw "Failed reading file";
      });
  }