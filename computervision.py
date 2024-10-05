import socket
import cv2
from cvzone.PoseModule import PoseDetector

# Set up the TCP server
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind(('localhost', 8080))  # Localhost address and port
server_socket.listen(1)  # Listen for one client
print("Waiting for a connection from Unity...")

# Initialize OpenCV and PoseDetector
cap = cv2.VideoCapture(0)
detector = PoseDetector()

# Wait for a connection from the Unity client
conn, addr = server_socket.accept()
print(f"Connected to {addr}")

while True:
    success, img = cap.read()
    if not success:
        print("Camera not detected.")
        break

    # Detect the pose and keypoints in real-time
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)

    if bboxInfo:
        lmString = ''
        # Ensure that lmList has valid keypoints before processing
        for lm in lmList:
            # Append keypoint information in the desired format
            lmString += f'{lm[0]},{img.shape[0] - lm[1]},{lm[2]},'

        if lmString:  # Only send data if lmString is not empty
            try:
                conn.sendall(lmString.encode('utf-8'))
            except Exception as e:
                print(f"Error sending data: {e}")

    # Show the real-time feed with pose detection
    cv2.imshow("Real-Time Pose Detection", img)

    key = cv2.waitKey(1)
    if key == ord('q'):  # Press 'q' to exit
        break

cap.release()
conn.close()
cv2.destroyAllWindows()
server_socket.close()
