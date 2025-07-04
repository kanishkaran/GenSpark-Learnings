const express = require('express');

const app = express();

const PORT  = 5000;

app.get('/hello', (req, res) => {
    res.send('Say Hello to react :> ');
})

app.listen(PORT, () => {
    console.log(`server is listening at port: ${PORT}`)
})