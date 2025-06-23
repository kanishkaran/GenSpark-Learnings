import { delay } from "rxjs";

addEventListener('message', ({ data }) => {
  const file: File = data.file;
  console.log("Worker Invoked")
  const reader = new FileReader();

  reader.onload = async () => {
  postMessage('Starting parsing...');
  await delay(2000);
  postMessage(reader.result);
};


  reader.onerror = () => {
    postMessage('ERROR: failed to read file');
  };

  reader.readAsText(file); 
});