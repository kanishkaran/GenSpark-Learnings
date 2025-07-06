const express = require('express');
const mongoose = require('mongoose');


const app = express();

const PORT = 5000;
console.log('🔍 MONGO_URI:', process.env.MONGO_URI);
const MONGO_URI = process.env.MONGO_URI || 'mongodb://mongo:27017/testDb';


let isConnected = false;

mongoose.connect(MONGO_URI)
    .then(() => {
        console.log('✅ MongoDB Connected');
        isConnected = true;
    })
    .catch(err => {
        console.error('❌ MongoDB Connection Failed:', err);
        isConnected = false;
    });

app.get('/api/db-status', (req, res) => {
    if (isConnected) {
        res.send('Connected');
    } else {
        res.send('Not connected');
    }
});

app.listen(PORT, () => {
    console.log(` Server running at http://localhost:${PORT}`);
});