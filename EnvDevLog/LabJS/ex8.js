async function capitalizeText(file) {
    let lines;
    try {
        lines = await readFile(file);
    } catch(error) {
        throw "Failed reading file";
    };

    lines = await removeMarks(lines);
    // Converting Promise.all to async syntax will cause the promises to
    // execute sequentially so we leave it in the same way
    const promisesArray = lines.forEach(createProcessLinePromise(line))
    return Promise.all[promisesArray];
}