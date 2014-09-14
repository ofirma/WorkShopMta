var express = require('express')
    , app = module.exports = express();
 
// Using the .html extension instead of
// having to name the views as *.ejs
app.engine('.html', require('ejs').__express);
 
// Set the folder where the pages are kept
app.set('views', __dirname + '/views');
 
// This avoids having to provide the 
// extension to res.render()
app.set('view engine', 'html');

app.use(express.static(__dirname + '/resources'));
 
// Serve the index page
app.get('/moses', function(req, res){
  res.render('1', {
    // PLACEHOLDER
    pageTitle: 'EJ21S Demo',
	moses: 'boom!',
	point1: '32.08018,34.78056'
  });
});
 
if (!module.parent) {
  app.listen(8080);
  console.log('EJS Demo server started on port 8080');
}