window.onload = function() {
     
	function onMouseUp(event) {
		document.body.removeEventListener("mousemove", onMouseMove);
		document.body.removeEventListener("mouseup", onMouseUp);	
		isDragging = false;
		draw();	
	}
};