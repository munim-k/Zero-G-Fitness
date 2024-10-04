import socket
import cv2
from cvzone.PoseModule import PoseDetector

# Set up the socket for TCP communication
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(('localhost', 8080))  # Use localhost for local communication
server_socket.listen(1)
conn, addr = server_socket.accept()

# OpenCV Pose Detector
cap = cv2.VideoCapture(0)
detector = PoseDetector()

while True:
    success, img = cap.read()
    if not success:
        break

    # Detect pose and extract keypoints
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)

    if bboxInfo:
        lmString = ''
        for lm in lmList:
            lmString += f'{lm[0]},{img.shape[0] - lm[1]},{lm[2]},'

        # Send the pose data to Unity
        conn.sendall(lmString.encode('utf-8'))

    # Break on pressing 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

conn.close()
cap.release()
cv2.destroyAllWindows()
